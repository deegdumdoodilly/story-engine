package com.storyengine.springboot;

import org.apache.catalina.core.ApplicationContext;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.PathVariable;

import org.springframework.web.server.ResponseStatusException;

import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;

@RestController
@RequestMapping(path="/api/votes")
public class VoteController {
  @Autowired
  private VoteRepository voteRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Vote> GetVotes (@RequestParam(required = false) Integer id){
    if(id == null){
      return voteRepository.findAll();
    }else{
      if(!voteRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find vote with id " + id);
      }
      ArrayList<Vote> result = new ArrayList<Vote>();
      result.add(voteRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Vote AddNewVote (@RequestBody Vote newVote) {
    System.out.println(newVote.toString());
    return voteRepository.save(newVote);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveVote (@RequestParam(required = false) Integer id) {
    if(id == null){
      for (Vote p : voteRepository.findAll()) {
        System.out.println("Deleting vote ID: " + p.getId());
        voteRepository.delete(p);
      }
      return "Votes removed.";
    }else{
      if(!voteRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find vote with id " + id);
      }
      voteRepository.deleteById(id);
      System.out.println("Deleting vote ID: " + id);
      return "Vote removed.";
    }
  }
  
  @PostMapping(path="/set-in-progress") 
  public @ResponseBody Vote SetVoteInProgress (@RequestParam(required = false) Integer id, @RequestBody String inProgress) {
    boolean inProg = false;
    if(inProgress.toLowerCase().equals("true")){
      inProg = true;
    }else if(!inProgress.toLowerCase().equals("false")){
      System.out.println("Unable to parse boolean");
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse boolean from '" + inProgress + "'', must be 'true' or 'false'.");
    }
    if(id == null){
      for(Vote vote : voteRepository.findAll()){
        vote.setInProgress(inProg);
        voteRepository.save(vote);
      }
      return null;
    }
    if(voteRepository.existsById(id)){
      Vote vote = voteRepository.findById(id).get();
      vote.setInProgress(inProg);
      voteRepository.save(vote);
      return voteRepository.findById(id).get();
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find vote with id " + id);
    }
  }

  @PostMapping(path="/set-chosen-outcome") 
  public @ResponseBody Vote SetVoteOutcome (@RequestParam Integer id, @RequestBody String outcomeId) {
    int oId = -1;
    try{
      oId = Integer.parseInt(outcomeId);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + outcomeId + "'");
    }
    if(voteRepository.existsById(id)){
      Vote vote = voteRepository.findById(id).get();
      if(vote.isInProgress()){
        vote.setChosenOutcomeId(oId);
        vote.hasChosenOutcome(true);
        voteRepository.save(vote);
        return voteRepository.findById(id).get();
      }else{  
        System.out.println("Error, vote is not editable at this time.");
        throw new ResponseStatusException(HttpStatus.METHOD_NOT_ALLOWED, "Vote is not listed as inProgress, and therefore cannot be edited.");
      }
    }else{
      return null;
    }
  }
}
