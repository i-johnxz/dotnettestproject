# start existing db to model
Scaffold-DbContext "Server=(localdb)\projects;Database=Blogging;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models