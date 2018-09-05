# MetropolisLauncher

Metropolis Launcher has been created to be a great old-school launcher, emulation front-end and an extensive offline database of video game metadata thanks to MobyGames.com and their strong user base.

Metropolis Launcher is based on the .NET 4.0 Framework and runs on Windows XP/Vista/7/8/8.1/10

Get an overview of the features at http://metropolis-launcher.net

# Build from Source

1. Clone the repository to your local machine

2. Fetch the latest binary release from https://github.com/theMK2k/MetropolisLauncher/releases

3. Extract the .db files from the latest binary release and put them into Metropolis_Launcher the subdirectory

This is unfortunately necessary, as these databases are too large for the github LFS free plan.

4. Either have a licensed DevExpress DXPerience distribution installed or extract the DevExpress.*.dll from the latest binary release and use these

If you take the approach of using extracted DevExpress.*.dll files, you won't be able to alter any forms/user controls.