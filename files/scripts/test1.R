# set output to file (DEBUG)
sink("c:\\in\\SPA\\r.out")

# file will contain output file for the script
print(file)

# params contain a vector with selected parameters
print(params)

# libraries
library(DBI)
library(odbc)
library(dplyr)
library(dbplyr)
library(xlsx)

# connect to database
db <- dbConnect(odbc(), "TMP", uid = "db2admin", pwd = "db2admin", encoding = "CP1251")

# table
users <- tbl(db, in_schema("ACCOUNT", "USERS"))

# query
q <- users %>%
    select(ID_USER, LOGIN, NAME)

# display SQL query
q %>% show_query()

# get data from query
res <- q %>% collect()

## write to xls file
#write.xlsx(res, file, sheetName = "Sheet name")

## append to xls file
template <- "test1.xlsx"
wb <- loadWorkbook(template)
shs <- getSheets(wb)
sh <- shs[[1]]
addDataFrame(res, sh, startRow=3, startColumn=1)
saveWorkbook(wb, file)

# disconnect
dbDisconnect(db)

# reset sink, probably unnecessary
sink()
