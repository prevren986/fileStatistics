package entityObjects;

public class Artist {
        public Artist(){
            
        }
        
        public Artist(String name){
            artistName = name;
        }
    
	public String getArtistName() {
		return artistName;
	}

	public void setArtistName(String artistName) {
		this.artistName = artistName;
	}

	private String artistName;
	

}
