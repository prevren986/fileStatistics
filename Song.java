package entityObjects;

import com.mpatric.mp3agic.InvalidDataException;
import java.sql.Time;
import com.mpatric.mp3agic.Mp3File;
import com.mpatric.mp3agic.ID3v1;
import com.mpatric.mp3agic.ID3v2;
import com.mpatric.mp3agic.UnsupportedTagException;
import java.io.File;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
//import com.mpatric.mp3agic.*;

public class Song {
	private String songName;

    
	private int songYear;
	private String songFilePath;
	private Time songDuration;
	private Album album;
	private Artist artist;
	private Genre genre;
	private Mp3File mp3file;
        private String comments;
        private Picture picture;
	//Key for delete as songs in a playlist can be duplicate
    private int songPlaylistID;
        public Song(){
            
        }
        
	public Song(String filepath){
            this.songFilePath = filepath;
            try {
                this.mp3file = new Mp3File(filepath);
                long msec = (long) 1000*this.mp3file.getLengthInSeconds();
                this.songDuration = new Time(msec);
                if (this.mp3file.hasId3v1Tag()){
                    ID3v1 id3v1Tag = this.mp3file.getId3v1Tag();
                    this.songName = id3v1Tag.getTitle();
                    this.songYear = Integer.parseInt(id3v1Tag.getYear());
                    this.album = new Album(id3v1Tag.getAlbum(),this.songYear);
                    this.artist = new Artist(id3v1Tag.getArtist());
                    this.genre = new Genre(id3v1Tag.getGenreDescription());
                    this.comments = id3v1Tag.getComment();
                    
                }
                else if (this.mp3file.hasId3v2Tag()){
                    ID3v2 id3v2Tag = this.mp3file.getId3v2Tag();
                    this.songName = id3v2Tag.getTitle();
                    this.songYear = Integer.parseInt(id3v2Tag.getYear());
                    this.album = new Album(id3v2Tag.getAlbum(),this.songYear);
                    this.artist = new Artist(id3v2Tag.getArtist());
                    this.genre = new Genre(id3v2Tag.getGenreDescription());
                    this.comments = id3v2Tag.getComment();
                }
            } catch (IOException ex) {
                Logger.getLogger(Song.class.getName()).log(Level.SEVERE, null, ex);
            } catch (UnsupportedTagException ex) {
                Logger.getLogger(Song.class.getName()).log(Level.SEVERE, null, ex);
            } catch (InvalidDataException ex) {
                Logger.getLogger(Song.class.getName()).log(Level.SEVERE, null, ex);
            }
	}
	
	public String getSongName() {
		return songName;
	}
	public void setSongName(String songName) {
		this.songName = songName;
	}
	public int getSongYear() {
		return songYear;
	}
	public void setSongYear(int songYear) {
		this.songYear = songYear;
	}
	public String getSongFilePath() {
		return songFilePath;
	}
	public void setSongFilePath(String songFilePath) {
		this.songFilePath = songFilePath;
	}
	public Time getSongDuration() {
		return songDuration;
	}
	public void setSongDuration(Time songDuration) {
		this.songDuration = songDuration;
	}
	public Album getAlbum() {
		return album;
	}
	public void setAlbum(Album album) {
		this.album = album;
	}
	public Artist getArtist() {
		return artist;
	}
	public void setArtist(Artist artist) {
		this.artist = artist;
	}
	public Genre getGenre() {
		return genre;
	}
	public void setGenre(Genre genre) {
		this.genre = genre;
	}
        public String getComments() {
        return comments;
        }

    public void setComments(String comments) {
        this.comments = comments;
    }
      public int getSongPlaylistID() {
        return songPlaylistID;
    }

    public void setSongPlaylistID(int songPlaylistID) {
        this.songPlaylistID = songPlaylistID;
    }
    
    public Picture getPicture() {
        return picture;
    }

    public void setPicture(Picture picture) {
        this.picture = picture;
    }

    
        
	

}
