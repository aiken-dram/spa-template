# IBM DB2 database

Open `SPA Data Model` project in `IBM Data Studio 4.1.3`

## List of SQL scripts:

- `CreateEmpty`
  - Creating database objects without SAMPLE
- `CreateSample`
  - Create database objects with SAMPLE
- `Initial`
  - Filling database tables with initial values
- `InitialSample`
  - Filling database tables with initial values for SAMPLE objects
- `Optimization`
  - Optimization script
- `Mock`
  - Script for filling tables with mock data for testing purposes

## Docker with DB2 11 Community edition

Pull image:

```sh
docker pull ibmcom/db2
```

Start container:

```sh
docker run -itd --name mydb2 --privileged=true -p 50000:50000 -e LICENSE=accept -e DB2INST1_PASSWORD=db2admin -e -v E:/database ibmcom/db2
```

Log on to container:

```sh
docker exec -ti mydb2 bash -c "su - db2inst1"
```

Create database for SPA:

```sh
db2 CREATE DATABASE SPA
```

After that connect to SPA database on localhost:50000 from `IBM Data Studio 4.1.3` and run `CreateSample`, `Initial` and `InitialSample` scripts
