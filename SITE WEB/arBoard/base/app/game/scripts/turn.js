function lancerDe(){
	let de = Math.floor((Math.random() * 6) + 1);
	if(document.getElementById('horse1').checked){
		_Board.move(1,de);
	}else{
		_Board.move(2,de);
	}
	resultId = document.getElementById("result-de");
	resultId.innerHTML = de;
	document.getElementById("form-conf-turn").className = "display-block";
}

function playTurn(){ // Tous les scripts de déplacement des chevaux
	// $("#plateau").html();
	let board = document.getElementById("boardValue").value;
	
	let newBoard = $("#plateau").html();
	endTurn(newBoard);
}

function endTurn(newBoard){
	document.getElementById("newboardvalue").value = newBoard;
}
