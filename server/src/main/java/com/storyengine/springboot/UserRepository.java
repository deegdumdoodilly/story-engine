package com.storyengine.springboot;

import com.storyengine.springboot.User;

import org.springframework.data.repository.CrudRepository;

public interface UserRepository extends CrudRepository<User, Integer> {

}
