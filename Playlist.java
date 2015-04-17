package entityObjects;

import java.sql.Time;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 *
 * @author Ariel
 */
public class Playlist {
    private String name;
    private List<Song> playlist;
    private int index;

  
    
    public Playlist()
    {
    }
    public Playlist(String n){
        name = n;
    }
    
    
    public void addSong(Song s){
        if (playlist == null) playlist = new ArrayList<>();
        playlist.add(s);
    }
    
    public void deleteSong(int i) {
        playlist.remove(i);
    }
    
    public void deleteSong(Song s){
        playlist.remove(s);
    }
    
    public void moveSong(int to, int from){
        Song temp = playlist.remove(from);
        playlist.add(to,temp);
    }
    
    public String getName(){
        return name;
    }
    
    public void setName(String s){
        name = s;
    }
    
    public int getIndex(){
        return index;
    }
    
    public void setIndex(int i){
        if(i < playlist.size()){
            index = i;
        }
        else if(i < 0){
            index = 0;
        }
        else if (i >= playlist.size()){
            index = playlist.size() - 1;
        }
    }
    
    public Song getSong(int i){
        return playlist.get(i);
    }
    
    public int next(){
        if(index == playlist.size()-1){
            index = 0;
        }
        else{
            index = index + 1;
        }
        return index;
    }
    
    public int prev(){
        if(index == 0){
            index = playlist.size() - 1;
        }
        else{
            index = index - 1;
        }
        return index;
    }
    
    public Song getCurrentSong(){
        return playlist.get(index);
    }
    
    public List<Song> getSongs(){
        return playlist;
    }
    
    public void setSongs(List<Song> l){
        playlist = l;
    }
    
    public void sortBy(String field){
        // Get values according to field being sorted
        String[] values = new String[playlist.size()];
        //List<String> values = new ArrayList<String>();
        int i = 0;
        for (Song s : playlist) {
//            if (field.equals("Title")){values.add(s.getSongName());}
//            else if (field.equals("Duration")){values.add(translateDuration(s));}
//            else if (field.equals("Artist")){values.add(s.getArtist().getArtistName());}
//            else if (field.equals("Album")){values.add(s.getAlbum().getAlbumName());}
//            else if (field.equals("Year")){values.add(String.valueOf(s.getSongYear()));}
//            else if (field.equals("Genre")){values.add(s.getGenre().getGenreName());}
//            else if (field.equals("Comments")){values.add(s.getComments());}
            if (field.equals("Title")){values[i] = s.getSongName().toLowerCase();}
            else if (field.equals("Duration")){values[i] = translateDuration(s);}
            else if (field.equals("Artist")){values[i] = s.getArtist().getArtistName().toLowerCase();}
            else if (field.equals("Album")){values[i] = s.getAlbum().getAlbumName().toLowerCase();}
            else if (field.equals("Year")){values[i] = String.valueOf(s.getSongYear());}
            else if (field.equals("Genre")){values[i] = s.getGenre().getGenreName().toLowerCase();}
            else if (field.equals("Comments")){values[i] = s.getComments().toLowerCase();}
            i++;
        }
        // Convert to ArrayIndexComparator
        //String[] stringArray = (String[]) values.toArray();
        ArrayIndexComparator comparator = new ArrayIndexComparator(values);
        Integer[] indexes = comparator.createIndexArray();
        Arrays.sort(indexes, comparator);
        // Rebuild playlist in sorted order
        List<Song> newOrder = new ArrayList<Song>();
        for( int in : indexes){
            newOrder.add(playlist.get(in));
        }
        playlist = newOrder;
        index = 0;
    }
    
    private String translateDuration (Song s)
        {
            int minutes = s.getSongDuration().getMinutes();
            int seconds = s.getSongDuration().getSeconds();
            String mmss = String.valueOf(minutes) + ":" + String.valueOf(seconds);
            return mmss;
        }
}
