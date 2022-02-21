package com.storyengine.springboot;

import com.storyengine.springboot.Requirement;

import org.springframework.data.repository.CrudRepository;

public interface RequirementRepository extends CrudRepository<Requirement, Integer> {

}
