ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC0724A8527E

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id, Username)