package com.storyengine.springboot;

import com.storyengine.springboot.Actor;

import org.springframework.data.repository.CrudRepository;

public interface ActorRepository extends CrudRepository<Actor, Integer> {

}
