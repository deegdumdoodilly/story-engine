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
@RequestMapping(path="/api/extraimages")
public class ExtraImageController {
  @Autowired
  private ExtraImageRepository extraImageRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<ExtraImage> GetExtraImages (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer sceneId){
    if(id == null){
      if(sceneId == null){
        return extraImageRepository.findAll();
      }else{
        Iterable<ExtraImage> allImages = extraImageRepository.findAll();
        ArrayList<ExtraImage> result = new ArrayList<ExtraImage>();
        for(ExtraImage e : allImages){
          if(e.getSceneId().intValue() == sceneId){
            result.add(e);
          }
        }
        return result;
      }
    }else{
      if(!extraImageRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find image with id " + id);
      }
      ArrayList<ExtraImage> result = new ArrayList<ExtraImage>();
      result.add(extraImageRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody ExtraImage AddNewExtraImage (@RequestBody ExtraImage newExtraImage) {
    System.out.println(newExtraImage.toString());
    return extraImageRepository.save(newExtraImage);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveExtraImage (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer sceneId) {
    if(id == null){
      if(sceneId == null){
        for (ExtraImage o : extraImageRepository.findAll()) {
          System.out.println("Deleting ExtraImage ID: " + o.getId());
          extraImageRepository.delete(o);
        }
        return "ExtraImages removed.";
      }else{
        for (ExtraImage o : extraImageRepository.findAll()) {
          if(o.getSceneId().intValue() == sceneId){
            System.out.println("Deleting ExtraImage ID: " + o.getId());
            extraImageRepository.delete(o);
          }
        }
        return "ExtraImages removed.";
      }
    }else{
      if(!extraImageRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find ExtraImage with id " + id);
      }
      extraImageRepository.deleteById(id);
      System.out.println("Deleting ExtraImage ID: " + id);
      return "ExtraImage removed.";
    }
  }
}
