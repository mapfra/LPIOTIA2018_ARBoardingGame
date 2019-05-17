class Horse {
	/* Classe pour chaque cheval du plateau */
    constructor(id, element, color) {
		/* constructeur de la classe Horse */
        this.position = element; /* Recupere la position du cheval lors de sa création */
        this.caseDepart = 0; /* Declaration de la varible pour la case de départ */
        this.id = id; /* Enregistre l'id du cheval */
        switch (color) { /* Met a jour la varible de la case de départ en fonction de la couleur du cheval */
            case "red":
                this.caseDepart = 29;
                break;
            case "blue":
                this.caseDepart = 15;
                break;
            case "green":
                this.caseDepart = 1;
                break;
            case "orange":
                this.caseDepart = 43;
                break;
            default:
                break;
        }
        this.defaultPosition = { /* Recupere la position de départ du cheval*/
            x: this.position.getAttribute('cx'),
            y: this.position.getAttribute('cy')
        }
        this.currentCase = -1; /* Définition de la variable pour savoir sur quelle case il est */
        this.x = 0; /* Définition de la variable qui est la position x de destination */
        this.y = 0; /* Définition de la variable qui est la position y de destination */
        this.color = color; /* Définition de la couleur du cheval */
        this.queue = []; /* La liste d'attente des déplacement du cheval a éffectuer*/
        this.isMoving = false; /* varible pour savoir si le cheval se déplace ou pas */
        this.destination = -1; /* Définition de la variable pour la case de destination */
        this.finalPath = -1; /* Définition de la variable pour savoir si le cheval a finit son tour du plateau et si oui a quelle case il est */
        this.win = false; /* Définition de la variable pour savoir si le cheval est arrivé a la fin */
    }

    goTo(direction) { /* Ajoute a la liste d'attente la direction du cheval ( "+" ou "-" ) */
        this.queue.push(direction);
    }

    reset(){/* Le cheval retourne à sa case de départ et réinitialise ses données  */
        this.position.setAttribute('cx', this.defaultPosition.x);
        this.position.setAttribute('cy', this.defaultPosition.y);
        this.currentCase = -1;
        this.queue = [];
        this.destination = -1;
        this.isMoving = false;
        this.finalPath = -1;
        this.x = 0;
        this.y = 0;
    }

    animate() { /* Anime le cheval pour aller a sa destination */
        if(this.queue.length === 0) return; /* Si il n'y a pas de destination dans la liste d'attente stop la suite */
		/* Récuperation de la position du cheval */
        const posActuelX = parseInt(this.position.getAttribute('cx'));
        const posActuelY = parseInt(this.position.getAttribute('cy'));
        document.getElementById("play").disabled = true;
		/* Deplace le cheval de 1 pixel si jusqu' à la destination puis le supprime de la liste et met la varible isMoving a false */
        if (this.x < parseInt(this.position.getAttribute('cx'))) {
            this.position.setAttribute("cx", posActuelX - 1);
            this.isMoving = true;
        } else if (this.x > parseInt(this.position.getAttribute('cx'))) {
            this.position.setAttribute("cx", posActuelX + 1);
            this.isMoving = true;
        } else if (this.y > parseInt(this.position.getAttribute('cy'))) {
            this.position.setAttribute("cy", posActuelY + 1);
            this.isMoving = true;
        } else if (this.y < parseInt(this.position.getAttribute('cy'))) {
            this.position.setAttribute("cy", posActuelY - 1);
            this.isMoving = true;
        } else if (posActuelY === this.y && posActuelX === this.x && this.isMoving) {
            this.currentCase = this.destination;
            this.queue.shift();
            this.isMoving = false;
        }
    }
}