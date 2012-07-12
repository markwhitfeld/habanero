require 'rake'
require 'albacore'

#______________________________________________________________________________
#---------------------------------SETTINGS-------------------------------------

# set up the build script folder so we can pull in shared rake scripts.
# This should be the same for most projects, but if your project is a level
# deeper in the repo you will need to add another ..
bs = File.dirname(__FILE__)
bs = File.join(bs, "..") if bs.index("branches") != nil
bs = File.join(bs, "../../HabaneroCommunity/BuildScripts")
$buildscriptpath = File.expand_path(bs)
$:.unshift($buildscriptpath) unless
    $:.include?(bs) || $:.include?($buildscriptpath)

if (bs.index("branches") == nil)	
	nuget_version = 'Trunk'
	nuget_version_id = '9.9.999'
	
	$nuget_publish_version = nuget_version
	$nuget_publish_version_id = nuget_version_id
else
	$nuget_publish_version = 'v2.6-13_02_2012'
	$nuget_publish_version_id = '2.6'
end		
	
#------------------------build settings--------------------------
require 'rake-settings.rb'

msbuild_settings = {
  :properties => {:configuration => :release},
  :targets => [:clean, :rebuild],
  :verbosity => :quiet,
  #:use => :net35  ;uncomment to use .net 3.5 - default is 4.0
}

#------------------------dependency settings---------------------

#------------------------project settings------------------------
$solution = 'source/Habanero.sln'

#______________________________________________________________________________
#---------------------------------TASKS----------------------------------------

desc "Runs the build task"
task :default => [:build]

desc "Builds Habanero, including tests and pushes to local nuget folder"
task :build => [:installNugetPackages, :build_only, :test, :nuget]

desc "Builds Habanero, including tests"
task :build_test => [:clean, :installNugetPackages, :msbuild, :test]

desc "Builds Habanero"
task :build_only => [:clean, :msbuild]

desc "Builds Habanero, including running tests with dotcover then pushes to the local nuget server"
task :build_with_coverage => [:installNugetPackages, :build_only, :test_with_coverage, :nuget]

desc "Pushes Habanero into the local nuget folder"
task :nuget => [:publishBaseNugetPackage, 
				:publishConsoleNugetPackage, 
				:publishDBNugetPackage, 
				:publishBONugetPackage ]
#------------------------build habanero --------------------

desc "Cleans the bin folder"
task :clean do
	puts cyan("Cleaning bin folder")
	FileUtils.rm_rf 'bin'
end

desc "Builds the solution with msbuild"
msbuild :msbuild do |msb| 
	puts cyan("Building #{$solution} with msbuild")
	msb.update_attributes msbuild_settings
	msb.solution = $solution
end

desc "Runs the tests"
nunit :test do |nunit|
	puts cyan("Running tests")
	nunit.assemblies testassemblies
end

def testassemblies
	['bin\Habanero.Test.dll',
	'bin\Habanero.Test.Bo.dll',
	'bin\Habanero.Test.Db.dll']
end

desc "Runs the tests with dotcover"
dotcover :test_with_coverage do |dc|
	puts cyan("Running tests with dotcover")
	dc.assemblies testassemblies
    dc.filters '+:module=*;class=*;function=*'
end

desc "Install nuget packages"
getnugetpackages :installNugetPackages do |ip|
    ip.package_names = ["nunit.framework"]
end

desc "Publish the Habanero.Base nuget package"
pushnugetpackages :publishBaseNugetPackage do |package|
  package.InputFileWithPath = "bin/Habanero.Base.dll"
  package.Nugetid = "Habanero.Base.#{$nuget_publish_version}"
  package.Version = $nuget_publish_version_id
  package.Description = "Habanero.Base"
end

desc "Publish the Habanero.BO nuget package"
pushnugetpackages :publishBONugetPackage do |package|
  package.InputFileWithPath = "bin/Habanero.BO.dll"
  package.Nugetid = "Habanero.BO.#{$nuget_publish_version}"
  package.Version = $nuget_publish_version_id
  package.Description = "Habanero.BO"
end

desc "Publish the Habanero.Console nuget package"
pushnugetpackages :publishConsoleNugetPackage do |package|
  package.InputFileWithPath = "bin/Habanero.Console.dll"
  package.Nugetid = "Habanero.Console.#{$nuget_publish_version}"
  package.Version = $nuget_publish_version_id
  package.Description = "Habanero.Console"
end

desc "Publish the Habanero.DB nuget package"
pushnugetpackages :publishDBNugetPackage do |package|
  package.InputFileWithPath = "bin/Habanero.DB.dll"
  package.Nugetid = "Habanero.DB.#{$nuget_publish_version}"
  package.Version = $nuget_publish_version_id
  package.Description = "Habanero.DB"
end