delete from `user`;

ALTER TABLE user auto_increment = 1;

insert into `user` (id, username, valid_voter, passhash) values (1, 'sansuki', true, 'C5-4A-9D-B0-03-5D-DF-3E-74-F1-E2-9C-A4-B0-95-A4-BD-BC-F4-FB-B8-CA-1C-2F-FB-D4-C0-E7-A8-E7-17-AF');
insert into `user` (id, username, valid_voter) values (2, 'deeg', true);
insert into `user` (id, username, valid_voter) values (3, 'Emma', true);
insert into `user` (id, username, valid_voter) values (4, 'HiddenDream', true);
insert into `user` (id, username, valid_voter) values (5, 'Riana', true);
insert into `user` (id, username, valid_voter) values (6, 'Nyiss', true);
insert into `user` (id, username, valid_voter) values (7, 'Shea', true);

select * from `user`;