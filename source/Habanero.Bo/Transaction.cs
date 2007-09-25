using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Habanero.Base.Exceptions;
using Habanero.DB;
using Habanero.Base;
using log4net;

namespace Habanero.BO
{
    /// <summary>
    /// Manages a collection of transactions to commit to the database
    /// </summary>
	public class Transaction : ITransactionCommitter, ITransaction
    {
        private static readonly ILog log = LogManager.GetLogger("Habanero.BO.Transaction");

        private IDatabaseConnection _databaseConnection;
		private SortedDictionary<string, ITransaction> _transactions= new SortedDictionary<string, ITransaction>();
        private List<ITransaction> _transactionList = new List<ITransaction>();
		private Guid _transactionId = Guid.NewGuid();
    	private int _numberOfRowsUpdated;

        /// <summary>
        /// Constructor to initialise a transaction
        /// </summary>
        public Transaction() : this(DatabaseConnection.CurrentConnection)
        {
        }

        /// <summary>
        /// Constructor to initialise a transaction with a specified
        /// database connection
        /// </summary>
        /// <param name="databaseConnection">A database connection</param>
        public Transaction(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
            ClearTransactionCol();
        }

        /// <summary>
        /// Adds an Itransaction object to the collection of transactions
        /// </summary>
        /// <param name="transaction">An Itransaction object</param>
        public void AddTransactionObject(ITransaction transaction)
        {
			////check if the transaction object is in a valid state before adding to the col
			//transaction.CheckPersistRules();
			transaction.AddingToTransaction(this);
            //if the transaction already exists then ignore
			if (!_transactions.ContainsKey(transaction.StrID()))
            {
				_transactions.Add(transaction.StrID(), transaction);
                _transactionList.Add(transaction);
            }
        }

        /// <summary>
        /// Clears the collection of transactions
        /// </summary>
        private void ClearTransactionCol()
        {
        	_numberOfRowsUpdated = 0;
            _transactions.Clear();
            _transactionList.Clear();
        }

        /// <summary>
        /// Rolls back all transactions in the collection
        /// </summary>
        public void CancelTransaction()
        {
            TransactionRolledBack();
            ClearTransactionCol();
        }
		
    	///<summary>
    	/// This returns the number of rows that were affected by the 
    	/// transaction
    	///</summary>
    	public int NumberOfRowsUpdated
    	{
    		get { return _numberOfRowsUpdated; }
    	}

    	/// <summary>
		/// Cancels edits for all transactions in the collection
		/// </summary>
		public void CancelEdits()
		{
			TransactionCancelEdits();
		}

    	
    	/// <summary>
        /// Commits all transactions in the collection to the database
        /// </summary>
		public void CommitTransaction()
    	{
    		IDbConnection connection = _databaseConnection.GetConnection();
    		//IDbConnection connection = DatabaseConnection.CurrentConnection.GetConnection();
    		connection.Open();
    		using (IDbTransaction dbTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
    		{
    			try
    			{
    				BeforeCommit();
    				SqlStatementCollection statementCollection = GetPersistSql();
    				if (statementCollection.Count > 0)
    				{
    					_numberOfRowsUpdated = _databaseConnection.ExecuteSql(statementCollection, dbTransaction);
    					dbTransaction.Commit();
    				}
    				else
    				{
    					_numberOfRowsUpdated = 0;
    				}
    				AfterCommit();
    			}
    			catch (Exception e)
    			{
    				log.Error("Error commiting transaction: " + Environment.NewLine +
    				          ExceptionUtilities.GetExceptionString(e, 4, true));
    				dbTransaction.Rollback();
    				TransactionRolledBack();
    				throw;
    			}
    			finally
    			{
    				if (connection != null && connection.State == ConnectionState.Open)
    				{
    					connection.Close();
    				}
    			}
    		}
    		TransactionCommited();
    		ClearTransactionCol();
    	}

    	#region ITransaction Implementation

		/// <summary>
        /// Carries out final steps on all transactions in the collection
        /// after they have been committed
        /// </summary>
        private void TransactionCommited()
        {
			foreach (ITransaction transaction in _transactionList)
            {
                transaction.TransactionCommitted();
            }
        }

		/// <summary>
		/// Returns the sql statement collection needed to carry out 
		/// persistance to the database</summary>
		/// <returns>Returns an ISqlStatementCollection object</returns>
		internal SqlStatementCollection GetPersistSql()
		{
			SqlStatementCollection statementCollection;
			statementCollection = new SqlStatementCollection();
			foreach (ITransaction transaction in _transactionList)
			{
				ISqlStatementCollection thisStatementCollection = transaction.GetPersistSql();
				statementCollection.Add(thisStatementCollection);
			}
			return statementCollection;
		}

		/// <summary>
		/// Rolls back all transactions in the collection
		/// </summary>
		private void TransactionRolledBack()
		{
			foreach (ITransaction transaction in _transactionList)
			{
				transaction.TransactionRolledBack();
			}
		}

		/// <summary>
		/// Cancels the edit
		/// </summary>
		private void TransactionCancelEdits()
		{
			foreach (ITransaction transaction in _transactionList)
			{
				transaction.TransactionCancelEdits();
			}
		}

		/// <summary>
		/// Carries out additional steps before committing changes to the
		/// database, and returns true if it is ok to continue.
		/// If false is returned, then the commit will be aborted.
		/// </summary>
		/// <returns>Returns an indication of whether it is 
		/// ok to continue with the commit.</returns>
		private void BeforeCommit()
		{
			foreach (ITransaction transaction in _transactionList)
			{
				transaction.BeforeCommit();
			}
		}

    	/// <summary>
    	/// Carries out additional steps after committing changes to the
    	/// database
    	/// </summary>
    	private void AfterCommit()
		{
			foreach (ITransaction transaction in _transactionList)
			{
				transaction.AfterCommit();
			}
		}
		

		#endregion //ITransaction interface

    	#region ITransaction Members

    	/// <summary>
    	/// Carries out final steps on all transactions in the collection
    	/// after they have been committed
    	/// </summary>
    	void ITransaction.TransactionCommitted()
    	{
			TransactionCommited();
    	}

    	/// <summary>
    	/// Returns the sql statement collection needed to carry out 
    	/// persistance to the database</summary>
    	/// <returns>Returns an ISqlStatementCollection object</returns>
    	ISqlStatementCollection ITransaction.GetPersistSql()
    	{
			return GetPersistSql();
    	}

		/// <summary>
		/// Notifies this ITransaction object that it has been added to the 
		/// specified Transaction object
		/// </summary>
		bool ITransaction.AddingToTransaction(ITransactionCommitter transaction)
		{
			return true;
		}

		///// <summary>
		///// Checks a number of rules, including concurrency, duplicates and
		///// duplicate primary keys
		///// </summary>
		//void ITransaction.CheckPersistRules()
		//{
		//    //All transactions have their persist rules checked when
		//    // they are added the the transaction, so there is nothing 
		//    // to be checked for this Transaction's transaction collection.
		//}

    	/// <summary>
    	/// Rolls back the transactions
    	/// </summary>
    	void ITransaction.TransactionRolledBack()
    	{
			TransactionRolledBack();
    	}

    	/// <summary>
    	/// Cancels the edit
    	/// </summary>
    	void ITransaction.TransactionCancelEdits()
    	{
			TransactionCancelEdits();
    	}

    	/// <summary>
    	/// Returns the ID as a string
    	/// </summary>
    	/// <returns>Returns a string</returns>
    	string ITransaction.StrID()
    	{
			return _transactionId.ToString();
    	}

		/// <summary>
		/// Carries out additional steps before committing changes to the
		/// database, and returns true if it is ok to continue.
		/// If false is returned, then the commit will be aborted.
		/// </summary>
		/// <returns>Returns an indication of whether it is 
		/// ok to continue with the commit.</returns>
		void ITransaction.BeforeCommit()
    	{
			BeforeCommit();
    	}

    	/// <summary>
    	/// Carries out additional steps after committing changes to the
    	/// database
    	/// </summary>
    	void ITransaction.AfterCommit()
    	{
			AfterCommit();
    	}

    	#endregion
    }
}