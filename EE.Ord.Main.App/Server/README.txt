add-migration -Name "Initial" -Project "EE.Ord.Main.App.Server" -StartUpProject "EE.Ord.Main.App.Server"
update-database -Project "EE.Ord.Main.App.Server" -StartUpProject "EE.Ord.Main.App.Server"