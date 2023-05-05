# RedisCacheMemoryDesign
One of the projects I worked on during my internship at Wipelot Technology.

## Details
This is an simple .Net6 application where I have used MSSQL as database and REDIS as cache memory.
I have developed the project in C# and used layered architecture. While adding and deleting new data in the project, both MSSQL and Redis memory are processed. When user gets data, the application first checks Redis as the cache memory, if can't find it, it checks if the data is in the database.
