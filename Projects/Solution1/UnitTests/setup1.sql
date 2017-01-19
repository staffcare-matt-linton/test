
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Accounts' AND TABLE_SCHEMA = 'dbo')
begin
delete from Accounts;
insert into accounts (id, name) values ('acc1','John Smith');
insert into accounts (id, name) values ('acc2','Jane Jones');
insert into accounts (id, name) values ('acc3','Brian Johnson');
insert into accounts (id, name) values ('acc4','Sue Smedley');
end


