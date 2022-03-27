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
@RequestMapping(path="/api/scenes")
public class SceneController {
  @Autowired
  private SceneRepository sceneRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Scene> GetScenes (@RequestParam(required = false) Integer id){
    if(id == null){
      return sceneRepository.findAll();
    }else{
      if(!sceneRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find scene with id " + id);
      }
      ArrayList<Scene> result = new ArrayList<Scene>();
      result.add(sceneRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/validate") 
  public @ResponseBody Scene ValidateScene (@RequestBody Scene newScene) {
    System.out.println(newScene.toString());
    return newScene;
  }

  @PostMapping(path="/add") 
  public @ResponseBody Scene AddNewScene (@RequestBody Scene newScene) {
    System.out.println(newScene.toString());
    if(sceneRepository.existsById(newScene.getId())){
      Scene oldScene = sceneRepository.findById(newScene.getId()).get();
      oldScene.CopyFrom(newScene);
      sceneRepository.save(oldScene);
      return newScene;
    }else{
      return sceneRepository.save(newScene);
    }
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveScene (@RequestParam(required = false) Integer id) {
    if(id == null){
      for (Scene p : sceneRepository.findAll()) {
        System.out.println("Deleting scene ID: " + p.getId());
        sceneRepository.delete(p);
      }
      return "Scenes removed.";
    }else{
      if(!sceneRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find scene with id " + id);
      }
      for (Scene s : sceneRepository.findAll()) {
        if (s.getParentSceneId().intValue() == id){
          s.setParentSceneId(-1);
          sceneRepository.save(s);
        }
      }
      sceneRepository.deleteById(id);
      System.out.println("Deleting scene ID: " + id);
      return "Scene removed.";
    }
  }

  @PostMapping(path="/reset") 
  public @ResponseBody String ResetSceneOccurrences () {
    for(Scene s : sceneRepository.findAll()){
      s.setOccurrences(0);
      sceneRepository.save(s);
    }
    return "Occurrence count reset.";
  }

  @PostMapping(path="/set-occurrences") 
  public @ResponseBody Scene SetOccurrences (@RequestParam Integer id, @RequestBody String occurrences) {
    int occ = -1;
    try{
      occ = Integer.parseInt(occurrences);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + occurrences + "'");
    }
    if(sceneRepository.existsById(id)){
      Scene scene = sceneRepository.findById(id).get();
      scene.setOccurrences(occ);
      sceneRepository.save(scene);
      return scene;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find scene with id " + id);
    }
  }
}
