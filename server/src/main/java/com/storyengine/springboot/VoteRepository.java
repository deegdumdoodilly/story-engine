package com.storyengine.springboot;

import com.storyengine.springboot.Vote;

import org.springframework.data.repository.CrudRepository;

public interface VoteRepository extends CrudRepository<Vote, Integer> {

}
