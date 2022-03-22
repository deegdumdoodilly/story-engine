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
@RequestMapping(path="/api/users")
public class UserController {
  @Autowired
  private UserRepository userRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<User> GetUsers (@RequestParam(required = false) Integer id, @RequestParam(required = false) String username){
    ArrayList<User> result = new ArrayList<User>();
    if(id == null){
      if(username == null){
        return userRepository.findAll();
      }else{
        for(User user : userRepository.findAll()){
          if(user.getUsername().equals(username)){
            result.add(user);
            return result;
          }
        }
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Error, unable to find a user with that name");
      }
    }else{
      if(!userRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find user with id " + id);
      }
      result.add(userRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody User AddNewUser (@RequestBody User newUser) {
    System.out.println(newUser.toString());
    return userRepository.save(newUser);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveUser (@RequestParam(required = false) Integer id) {
    if(id == null){
      for (User p : userRepository.findAll()) {
        System.out.println("Deleting user ID: " + p.getId());
        userRepository.delete(p);
      }
      return "Users removed.";
    }else{
      if(!userRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find user with id " + id);
      }
      userRepository.deleteById(id);
      System.out.println("Deleting user ID: " + id);
      return "User removed.";
    }
  }

  @PostMapping(path="/reset") 
  public @ResponseBody String ResetUserVotes () {
    for(User u : userRepository.findAll()){
      u.setVotingChances(0);
      u.setPositiveVotes(0);
      u.setNeutralVotes(0);
      u.setNegativeVotes(0);
      userRepository.save(u);
    }
    return "Voting history reset for all users.";
  }

  @PostMapping(path="/set-voting-chances") 
  public @ResponseBody User SetVotingChances (@RequestParam Integer id, @RequestBody String votingChances) {
    int chances = -1;
    try{
      chances = Integer.parseInt(votingChances);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + votingChances + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setVotingChances(chances);
      userRepository.save(user);
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }

  @PostMapping(path="/set-positive-votes") 
  public @ResponseBody User SetPositiveVotes (@RequestParam Integer id, @RequestBody String numVotes) {
    int num = -1;
    try{
      num = Integer.parseInt(numVotes);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + numVotes + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setPositiveVotes(num);
      userRepository.save(user);
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }

  @PostMapping(path="/set-negative-votes") 
  public @ResponseBody User SetNegativeVotes (@RequestParam Integer id, @RequestBody String numVotes) {
    int num = -1;
    try{
      num = Integer.parseInt(numVotes);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + numVotes + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setNegativeVotes(num);
      userRepository.save(user);
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }

  @PostMapping(path="/set-neutral-votes") 
  public @ResponseBody User SetNeutralVotes (@RequestParam Integer id, @RequestBody String numVotes) {
    int num = -1;
    try{
      num = Integer.parseInt(numVotes);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + numVotes + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setNeutralVotes(num);
      userRepository.save(user);
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }

  @PostMapping(path="/modify-positive-votes") 
  public @ResponseBody User ModifyPositiveVotes (@RequestParam Integer id, @RequestBody String modification) {
    int num = -1;
    try{
      num = Integer.parseInt(modification);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + modification + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setPositiveVotes(user.getPositiveVotes() + num);
      userRepository.save(user);
      System.out.println(user.toString());
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }

  @PostMapping(path="/modify-negative-votes") 
  public @ResponseBody User ModifyNegativeVotes (@RequestParam Integer id, @RequestBody String modification) {
    int num = -1;
    try{
      num = Integer.parseInt(modification);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + modification + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setNegativeVotes(user.getNegativeVotes() + num);
      userRepository.save(user);
      System.out.println(user.toString());
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }

  @PostMapping(path="/modify-neutral-votes") 
  public @ResponseBody User ModifyNeutralVotes (@RequestParam Integer id, @RequestBody String modification) {
    int num = -1;
    try{
      num = Integer.parseInt(modification);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + modification + "'");
    }
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setNeutralVotes(user.getNeutralVotes() + num);
      userRepository.save(user);
      System.out.println(user.toString());
      return user;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find user with id " + id);
    }
  }
}
