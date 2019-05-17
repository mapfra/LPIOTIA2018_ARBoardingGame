class Board {
	/* Classe pour gerer la partie et les petits chevaux */
    constructor(){
		/* constructeur de la classe Board */
        this.horses = []; /* Déclaration du array qui contient les chevaux */
        this.timer = setInterval(this.update.bind(this), 2);/* appel de la fonction update toute les 2 ms */
        this.playerTurn = "green";/* Déclare la variable pour savoir la couleur du joueur qui joue et la met la valeur "green" */
        this.playerText = "Joueur vert joue"; /* Déclare la variable pour afficher quelle joueur joue */
        /* Déclare la couleur du cheval selon son id*/
		this.playerHorses = {
            green: [1,5],
            blue: [2,6],
            red: [3,7],
            orange: [4,8]
        };
		 /* Déclare la liste des chevaux actif */
        this.playerEnabled = {
            green: [true, true],
            blue: [true, true],
            red: [true, true],
            orange: [true, true]
        }
    }

	/* Lorsque'on change le tour du joueur */
    nextTurn(){
        switch(this.playerTurn){
			/* Change la varible du joueur qui joue et la varible du texte qui l'affiche */
            case "green":
                this.playerTurn = "blue";
                this.playerText = "Joueur bleue joue";
                break;
            case "blue":
                this.playerTurn = "red";
                this.playerText = "Joueur rouge joue";
                break;
            case "red":
                this.playerTurn = "orange";
                this.playerText = "Joueur orange joue";
                break;
            case "orange":
                this.playerTurn = "green";
                this.playerText = "Joueur vert joue";
                break;
        }
		/* Par défaut coche le premier chaval */
        document.getElementById("horse1").checked = true;
		/* Deactive le chaval 1 si le cheval 1 du joueur en cours n'est pas actif (déjà gagné)*/
        document.getElementById("horse1").disabled = !this.playerEnabled[this.playerTurn][0];
        if(!this.playerEnabled[this.playerTurn][0]){
			/* Si le cheval 1 du joueurs en cours est déactvé le chaval 2 est cocher */
            document.getElementById("horse2").checked = true;
        }
		
		/* Deactive le chaval 2 si le cheval 2 du joueur en cours n'est pas actif (déjà gagné)*/
        document.getElementById("horse2").disabled = !this.playerEnabled[this.playerTurn][1];
        if(!this.playerEnabled[this.playerTurn][1]){
			/* Si le cheval 2 du joueurs en cours est déactvé le chaval 1 est cocher */
            document.getElementById("horse1").checked = true;
        }
		
		/* Affiche  Le joueur qui doit jouer avec sa couleur */
        document.getElementById("playerText").innerText = this.playerText;
        document.getElementById("playerText").style.color = this.playerTurn;
    }
	
	/* Ajout d'un cheval avec son id et sa couleur */
    addHorse(element, color){
		/* instanciation d'un cheval dans le tableau des chevaux */
        this.horses.push(new Horse(this.horses.length + 1, document.getElementById(element), color));
    }

	/* Mis a jour pour tout les chevaux */
    update(){
		/* actualise la destination et l'anime pour tout les chevaux */
        this.horses.forEach((horse) => {
            this.refreshDestination(horse);
            horse.animate();
        });
    }

	/* actualise la destination */
    refreshDestination(horse){
		/* Si le chaval n'est pas déjà en mouvement */
        if (!horse.isMoving) {
			/* Met par défaut en destination la case actuel */
            horse.destination = horse.currentCase;
            if (horse.currentCase === -1 ){
				/* Si la destination est -1 le cheval va sur sa case de départ */
                horse.destination = horse.caseDepart;
            } else if (horse.queue[0] === "+") {
				/* Si dans la liste d'attente le cheval doit avancer la destination augmente d'une case */
                horse.destination++;
            } else if (horse.queue === "-") {
				/* Si dans la liste d'attente le cheval doit reculer la destination baisse d'une case*/
                horse.destination--;
            } else {
                document.getElementById("play").disabled = false;
                return;
            }

			/* Si le cheval a fait le tour du plateau il va au début */
            if (horse.destination === 57) horse.destination = 1;
			/* Si le cheval est au début et va avant il va a la fin */
            if (horse.destination === 0) horse.destination = 56;
			
			/* Vérification si il y a un cheval sur la destination */
            let otherHorse = this.checkForHorse(horse);

			/* Si il y en a un il retourn à sa case départ */
            if(otherHorse && horse.queue.length == 1 ){
                otherHorse.reset();
            }

            let element = document.getElementById(horse.destination);

			/* Si le chaval a fait 1 tour du plateau il va vers le centre*/
            if(horse.caseDepart - 1 === horse.currentCase || (horse.caseDepart === 1 && horse.currentCase === 56) ){
                horse.finalPath = 1;
            }
			
			/* Si le cheval  va vers le centre*/
            if(horse.finalPath !== -1){
                if(horse.finalPath < 6) {
					/* Si le cheval n'est pas sur la deriere case il avance */
                    horse.finalPath++;
                } else {
					/* Si il est sur la dernière case */
                    if(horse.queue.length > 1){
						/* si il va plus loin que le centre il recule */
                        horse.finalPath = 8-horse.queue.length;
                        horse.queue = ["-"];
                    }else{
						/* si il va sur le centre il a gangné*/
                        horse.finalPath = -1;
                        horse.win = true;
                        if(horse.id <= 4){
							/* Si c'est le cheval 1 qui a gagné */
							/* Déactive le cheval 1 */
                            this.playerEnabled[horse.color][0] = false;
                        }else{
							/* Si c'est le cheval 2 qui a gagné */                  
							/* Déactive le cheval 2 */
                            this.playerEnabled[horse.color][1] = false;
                        }
                        if(this.playerEnabled[horse.color][0] === false && this.playerEnabled[horse.color][1] === false){
							/* Si les 2 chevaux ont gagné */
							/* Affichage du gagnant et déactivation du boutton play */
                            document.getElementById("play").disabled = true;
                            document.getElementById("play").hidden = true;
                            document.getElementById("playerText").innerText = "Le joueur " + this.playerText + " a gagné !!!!!!";
                        }
                    }
                }
                if (horse.finalPath !== -1)
					/* Si le cheval va vers le centre il va sur la case de sa couleur */
                element = document.getElementById(horse.color + horse.finalPath);
            }
			
			/* si le cheval a gagner va au centre */
            if(horse.win){
                element = document.getElementById("center");
            }
	
			/* Si la destination est un cercle on récupère son centre et on change la position du cheval*/
            if (element.classList.contains("cercle")) {
                horse.x = parseInt(element.getAttribute('cx'));
                horse.y = parseInt(element.getAttribute('cy'));
            } else {
				/* Sinon on calcul son centre et on change la position du cheval */
                horse.x = parseInt(element.getAttribute('x')) + parseInt(element.getAttribute('width')) / 2;
                horse.y = parseInt(element.getAttribute('y')) + parseInt(element.getAttribute('height')) / 2;
            }
        }
    }
	
	/* Déplacement d'un cheval */
    move(horse, number){
		/* Affiche le score effectué */
        if(number === 6){
            document.getElementById("dice").innerText = "Vous avez fait " + number + " 😀";
            document.getElementById("dice").style.color = "gold";
        }else{
            document.getElementById("dice").innerText = "Vous avez fait " + number;
            document.getElementById("dice").style.color = "black";
        }
		/* Recupere le cheval dans la liste */
        let _horse = this.horses[this.playerHorses[this.playerTurn][horse-1]-1];
		/* Si le chaval a gagné stop la suite*/
        if(_horse.win) return;
		/* Si le cheval est dans l'écurie (case de départ)*/
        if(_horse.currentCase === -1) {
            if(number === 6){
				/* Si il a fait 6 il avance d'une case et peut rejouer */
                _horse.goTo("+");
                document.getElementById("play").disabled = true;
            }else{
				/* Sinon c'est au joueur suivant */
                this.nextTurn();
                document.getElementById("play").disabled = false;
            }
        }else{
			/* Si le cheval n'est pas dans l'écurie (case de départ)*/
			/* Ajoute le nombre de fois "+" pour avancer du meme nombre de case */
            for (let i = 0; i < number; i++) {
                _horse.goTo("+");
                document.getElementById("play").disabled = true;
            }
			/* Si le joueur n'a pas fait 6 c'est au joueur suivant de jouer */
            if (number !== 6) {
                this.nextTurn();
            }
        }
    }

	/* Vérification de la présence d'un cheval sur la case de destination */
    checkForHorse(horse){
        let result = null;
		/* Pour chaque cheval si un cheval se trouve sur la case renvoie le cheval */
        this.horses.forEach((_horse) => {
            if(horse.id !== _horse.id && _horse.currentCase !== -1 && horse.currentCase !== -1 && horse.destination === _horse.currentCase && _horse.finalPath === -1 && horse.finalPath === -1 && _horse.win === false && horse.win === false) {
                result = _horse;
            }
        })
        return result;
    }
}