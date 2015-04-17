package entityObjects;

public class Genre {
        public Genre(){

        }
        
	public Genre(String name){
            genreName = name;
        }

	public String getGenreName() {
		return genreName;
	}

	public void setGenreName(String genreName) {
		this.genreName = genreName;
	}

	private String genreName;
}
