class Board {
    constructor(){
        this.horses = [];
        this.timer = setInterval(this.update.bind(this), 20);
        this.playerTurn = "green";
        this.playerText = "Joueur vert joue";
        this.playerHorses = {
            green: [1,5],
            blue: [2,6],
            red: [3,7],
            orange: [4,8]
        };
        this.playerEnabled = {
            green: [true, true],
            blue: [true, true],
            red: [true, true],
            orange: [true, true]
        }
    }

    nextTurn(){
        /*switch(this.playerTurn){
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
        document.getElementById("horse1").checked = true;
        document.getElementById("horse1").disabled = !this.playerEnabled[this.playerTurn][0];
        if(!this.playerEnabled[this.playerTurn][0]){
            document.getElementById("horse2").checked = true;
        }
        document.getElementById("horse2").disabled = !this.playerEnabled[this.playerTurn][1];
        if(!this.playerEnabled[this.playerTurn][1]){
            document.getElementById("horse1").checked = true;
        }
        document.getElementById("playerText").innerText = this.playerText;
        document.getElementById("playerText").style.color = this.playerTurn;*/
    }

    addHorse(element, color){
        this.horses.push(new Horse(this.horses.length + 1, document.getElementById(element), color));
    }

    update(){
        this.horses.forEach((horse) => {
            this.refreshDestination(horse);
            horse.animate();
        });
    }

    refreshDestination(horse){
        if (!horse.isMoving) {
            horse.destination = horse.currentCase;
            if (horse.currentCase === -1 ){
                horse.destination = horse.caseDepart;
            } else if (horse.queue[0] === "+") {
                horse.destination++;
            } else if (horse.queue === "-") {
                horse.destination--;
            } else {
                //document.getElementById("play").disabled = false;
                return;
            }

            if (horse.destination === 57) horse.destination = 1;
            if (horse.destination === 0) horse.destination = 56;

            let otherHorse = this.checkForHorse(horse);

            if(otherHorse && horse.queue.length == 1 ){
                otherHorse.reset();
            }

            let element = document.getElementById(horse.destination);

            if(horse.caseDepart - 1 === horse.currentCase || (horse.caseDepart === 1 && horse.currentCase === 56) ){
                horse.finalPath = 1;
            }

            if(horse.finalPath !== -1){
                if(horse.finalPath < 6) {
                    horse.finalPath++;
                } else {
                    if(horse.queue.length > 1){
                        horse.finalPath = 6-horse.queue.length;
                        if(horse.finalPath < 1){
                            horse.finalPath = 1;
                        }
                        horse.queue = ["-"];
                    }else{
                        horse.finalPath = -1;
                        horse.win = true;
                        if(horse.id <= 4){
                            if(this.playerHorses[horse.color][1].win){
                                //document.getElementById("play").disabled = true;
                                document.getElementById("playerText").innerText = "Le joueur " + this.playerText + " a gagné !!!!!!";
                            }
                            this.playerEnabled[horse.color][0] = false;
                        }else{
                            if(this.playerHorses[horse.color][0].win){
                               // document.getElementById("play").disabled = true;
                                document.getElementById("playerText").innerText = "Le joueur " + this.playerText + " a gagné !!!!!!";
                            }
                            this.playerEnabled[horse.color][1] = false;
                        }
                        if(this.playerEnabled[horse.color][0] === false && this.playerEnabled[horse.color][1] === false){
                            //document.getElementById("play").disabled = true;
                            //document.getElementById("play").hidden = true;
                            document.getElementById("playerText").innerText = "Le joueur " + this.playerText + " a gagné !!!!!!";
                        }
                    }
                }
                if (horse.finalPath !== -1)
                element = document.getElementById(horse.color + horse.finalPath);
            }

            if(horse.win){
                element = document.getElementById("center");
            }

            if (element.classList.contains("cercle")) {
                horse.x = parseInt(element.getAttribute('cx'));
                horse.y = parseInt(element.getAttribute('cy'));
            } else {
                horse.x = parseInt(element.getAttribute('x')) + parseInt(element.getAttribute('width')) / 2;
                horse.y = parseInt(element.getAttribute('y')) + parseInt(element.getAttribute('height')) / 2;
            }
        }
    }

    move(horse, number){
        /*if(number === 6){
            document.getElementById("dice").innerText = "Vous avez fait " + number + " 😀";
            document.getElementById("dice").style.color = "gold";
        }else{
            document.getElementById("dice").innerText = "Vous avez fait " + number;
            document.getElementById("dice").style.color = "black";
        }*/
        let _horse = this.horses[this.playerHorses[this.playerTurn][horse-1]-1];
        if(_horse.win) return;
        if(_horse.currentCase === -1) {
            if(number === 6){
                _horse.goTo("+");
               // document.getElementById("play").disabled = true;
            }else{
                this.nextTurn();
               // document.getElementById("play").disabled = false;
            }
        }else{
            for (let i = 0; i < number; i++) {
                _horse.goTo("+");
               // document.getElementById("play").disabled = true;
            }
            if (number !== 6) {
                this.nextTurn();
            }
        }
    }

    checkForHorse(horse){
        let result = null;
        this.horses.forEach((_horse) => {
            if(horse.id !== _horse.id && _horse.currentCase !== -1 && horse.currentCase !== -1 && horse.destination === _horse.currentCase && _horse.finalPath === -1 && horse.finalPath === -1 && _horse.win === false && horse.win === false) {
                result = _horse;
            }
        })
        return result;
    }
}