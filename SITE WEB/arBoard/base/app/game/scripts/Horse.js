class Horse {
    constructor(id, element, color) {
        this.position = element;
        this.caseDepart = 0;
        this.id = id;
        switch (color) {
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
        this.defaultPosition = {
            x: this.position.getAttribute('cx'),
            y: this.position.getAttribute('cy')
        }
        this.currentCase = -1;
        this.x = 0;
        this.y = 0;
        this.color = color;
        this.queue = [];
        this.timer = setInterval(this.animate.bind(this), 2);
        this.isMoving = false;
        this.destination = -1;
        this.finalPath = -1;
        this.win = false;
    }

    goTo(direction) {
        this.queue.push(direction);
    }

    reset(){
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

    animate() {
        if(this.queue.length === 0) return;
        const posActuelX = parseInt(this.position.getAttribute('cx'));
        const posActuelY = parseInt(this.position.getAttribute('cy'));
        //document.getElementById("play").disabled = true;
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