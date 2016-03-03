public enum Directions{N,NE,E,SE,S,SW,W,NW}
public class Position  {

	int x;
	int y;

	public Position(int x, int y){
		this.x = x;
		this.y = y;
	}

	public void Set(int x, int y){
		this.x = x;
		this.y = y;
	}

	public int GetX(){
		return x;
	}

	public int GetY(){
		return y;
	}

	public void Move(Directions dir){
		switch (dir) {
		case Directions.N:
			Set (x, y - 1);
			return;
		case Directions.NE:
			Set (x + 1, y - 1);
			return;
		case Directions.E:
			Set (x + 1, y);
			return;
		case Directions.SE:
			Set (x + 1, y + 1);
			return;
		case Directions.S:
			Set (x, y + 1);
			return;
		case Directions.SW:
			Set (x - 1, y + 1);
			return;
		case Directions.W:
			Set (x - 1, y);
			return;
		case Directions.NW:
			Set (x - 1, y - 1);
			return;
		}
	}
	public override string ToString (){
		string letters = "ABCDEFGH";
		return "" + letters [x] + y;
	} 
}