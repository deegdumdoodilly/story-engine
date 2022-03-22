delete from `user`;

ALTER TABLE user auto_increment = 1;

insert into `user` (id, username, valid_voter) values (1, 'sansuki', true);
insert into `user` (id, username, valid_voter) values (2, 'deeg', true);
insert into `user` (id, username, valid_voter) values (3, 'Emma', true);
insert into `user` (id, username, valid_voter) values (4, 'HiddenDream', true);
insert into `user` (id, username, valid_voter) values (5, 'Riana', true);
insert into `user` (id, username, valid_voter) values (6, 'Nyiss', true);
insert into `user` (id, username, valid_voter) values (7, 'Shea', true);

select * from `user`;