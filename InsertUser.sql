delete from users;

ALTER TABLE users auto_increment = 1;

insert into users (username, passhash) values ('sansuki', 'C5-4A-9D-B0-03-5D-DF-3E-74-F1-E2-9C-A4-B0-95-A4-BD-BC-F4-FB-B8-CA-1C-2F-FB-D4-C0-E7-A8-E7-17-AF');
insert into users (username) values ('deeg');
insert into users (username) values ('Emma');
insert into users (username) values ('HiddenDream');
insert into users (username) values ('Riana');
insert into users (username) values ('Nyiss');
insert into users (username) values ('Shea');

select * from users;