<?php
    include('../../include/connexionBDD.php');
?>
<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <title>Ar Boarding Game</title>
        <link rel="stylesheet" type="text/css" href="../../css/generic.css">
    </head>
    <body>
        <?php
//            require('objects/game.php');
//
//            $newGame = new game();
//            $newGame->course();
            
        $n = 20; 
        $idlob = bin2hex(random_bytes($n));
        
        $gamepic = '<svg width="665" height="665" id="svg">
        // 1ère colonne
        <rect id="1" x="269" y="622" width="32" height="32" stroke-width="4" stroke="green" fill="white"></rect>
        <circle class="cercle" id="27" cx="285" cy="27" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="26" cx="285" cy="70" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="25" cx="285" cy="113" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="24" cx="285" cy="156" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="23" cx="285" cy="199" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="22" cx="285" cy="242" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="21" cx="285" cy="285" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
    
        <circle class="cercle" id="7" cx="285" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="6" cx="285" cy="424" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="5" cx="285" cy="467" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="4" cx="285" cy="510" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="3" cx="285" cy="553" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="2" cx="285" cy="596" r="17" stroke-width="4" stroke="green" fill="white"></circle>
    
        // 2ème colonne
        <rect id="29" x="365" y="10" width="32" height="32" stroke-width="4" stroke="red" fill="white"></rect>
        <circle class="cercle" id="30" cx="381" cy="70" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="31" cx="381" cy="113" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="32" cx="381" cy="156" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="33" cx="381" cy="199" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="34" cx="381" cy="242" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="35" cx="381" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
    
        <circle class="cercle" id="49" cx="381" cy="381" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="50" cx="381" cy="424" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="51" cx="381" cy="467" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="52" cx="381" cy="510" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="53" cx="381" cy="553" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="54" cx="381" cy="596" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="55" cx="381" cy="639" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
    
        // 1er ligne
        <rect id="15" x="10" y="270" width="32" height="32" stroke-width="4" stroke="blue" fill="white"></rect>
        <circle class="cercle" id="16" cx="70" cy="285" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="17" cx="113" cy="285" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="18" cx="156" cy="285" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="19" cx="199" cy="285" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <circle class="cercle" id="20" cx="242" cy="285" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
    
        <circle class="cercle" id="36" cx="424" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="37" cx="467" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="38" cx="510" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="39" cx="553" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="40" cx="596" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <circle class="cercle" id="41" cx="639" cy="285" r="17" stroke-width="4" stroke="red" fill="white"></circle>
    
    
        // 2ème ligne
        <rect id="43" x="622" y="365" width="32" height="32" stroke-width="4" stroke="orange" fill="white"></rect>
        <circle class="cercle" id="13" cx="25" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="12" cx="70" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="11" cx="113" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="10" cx="156" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="9" cx="199" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
        <circle class="cercle" id="8" cx="242" cy="381" r="17" stroke-width="4" stroke="green" fill="white"></circle>
    
        <circle class="cercle" id="48" cx="424" cy="381" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="47" cx="467" cy="381" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="46" cx="510" cy="381" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="45" cx="553" cy="381" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
        <circle class="cercle" id="44" cx="596" cy="381" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
    
    
        <circle class="cercle" id="28" cx="333" cy="27" r="17" stroke-width="4" stroke="red" fill="white"></circle>
        <rect id="red1" x="313" y="55" width="40" height="30" stroke-width="4" stroke="red" fill="white"></rect>
        <rect id="red2" x="313" y="98" width="40" height="30" stroke-width="4" stroke="red" fill="white"></rect>
        <rect id="red3" x="313" y="141" width="40" height="30" stroke-width="4" stroke="red" fill="white"></rect>
        <rect id="red4" x="313" y="184" width="40" height="30" stroke-width="4" stroke="red" fill="white"></rect>
        <rect id="red5" x="313" y="227" width="40" height="30" stroke-width="4" stroke="red" fill="white"></rect>
        <rect id="red6" x="313" y="270" width="40" height="30" stroke-width="4" stroke="red" fill="white"></rect>
    
        <rect id="green6" x="313" y="365" width="40" height="30" stroke-width="4" stroke="green" fill="white"></rect>
        <rect id="green5" x="313" y="408" width="40" height="30" stroke-width="4" stroke="green" fill="white"></rect>
        <rect id="green4" x="313" y="451" width="40" height="30" stroke-width="4" stroke="green" fill="white"></rect>
        <rect id="green3" x="313" y="494" width="40" height="30" stroke-width="4" stroke="green" fill="white"></rect>
        <rect id="green2" x="313" y="537" width="40" height="30" stroke-width="4" stroke="green" fill="white"></rect>
        <rect id="green1" x="313" y="580" width="40" height="30" stroke-width="4" stroke="green" fill="white"></rect>
        <circle class="cercle" id="56" cx="333" cy="639" r="17" stroke-width="4" stroke="green" fill="white"></circle>
    
        <circle class="cercle" id="14" cx="25" cy="333" r="17" stroke-width="4" stroke="blue" fill="white"></circle>
        <rect id="blue1" x="55" y="313" width="30" height="40" stroke-width="4" stroke="blue" fill="white"></rect>
        <rect id="blue2" x="98" y="313" width="30" height="40" stroke-width="4" stroke="blue" fill="white"></rect>
        <rect id="blue3" x="141" y="313" width="30" height="40" stroke-width="4" stroke="blue" fill="white"></rect>
        <rect id="blue4" x="184" y="313" width="30" height="40" stroke-width="4" stroke="blue" fill="white"></rect>
        <rect id="blue5" x="227" y="313" width="30" height="40" stroke-width="4" stroke="blue" fill="white"></rect>
        <rect id="blue6" x="270" y="313" width="30" height="40" stroke-width="4" stroke="blue" fill="white"></rect>
    
        <rect id="orange6" x="366" y="313" width="30" height="40" stroke-width="4" stroke="orange" fill="white"></rect>
        <rect id="orange5" x="409" y="313" width="30" height="40" stroke-width="4" stroke="orange" fill="white"></rect>
        <rect id="orange4" x="452" y="313" width="30" height="40" stroke-width="4" stroke="orange" fill="white"></rect>
        <rect id="orange3" x="495" y="313" width="30" height="40" stroke-width="4" stroke="orange" fill="white"></rect>
        <rect id="orange2" x="538" y="313" width="30" height="40" stroke-width="4" stroke="orange" fill="white"></rect>
        <rect id="orange1" x="581" y="313" width="30" height="40" stroke-width="4" stroke="orange" fill="white"></rect>
        <circle class="cercle" id="42" cx="639" cy="333" r="17" stroke-width="4" stroke="orange" fill="white"></circle>
    
        <rect id="center" x="313" y="313" width="40" height="40" stroke-width="4" stroke="white" fill="white"></rect>
    
        // Zone des joueurs
        <rect x="10" y="10" width="250" height="250" stroke-width="4" stroke="blue" fill="#0000ff8f"></rect>
        <rect x="405" y="10" width="250" height="250" stroke-width="4" stroke="red" fill="#ff00008c"></rect>
        <rect x="10" y="405" width="250" height="250" stroke-width="4" stroke="green" fill="#0080008f"></rect>
        <rect x="405" y="405" width="250" height="250" stroke-width="4" stroke="orange" fill="#ffa50085"></rect>
        <line x1="20" y1="80" x2="130" y2="200" fill="red"></line>
    
        <circle id="Horse1" cx="242" cy="639" r="10" style="fill: green;"></circle>
        <circle id="Horse5" cx="199" cy="639" r="10" style="fill: green;"></circle>
    
        <circle id="Horse2" cx="25" cy="242" r="10" style="fill: blue;"></circle>
        <circle id="Horse6" cx="25" cy="199" r="10" style="fill: blue;"></circle>
    
        <circle id="Horse3" cx="424" cy="27" r="10" style="fill: #F70701;"></circle>
        <circle id="Horse7" cx="467" cy="27" r="10" style="fill: #F70701;"></circle>
    
        <circle id="Horse4" cx="639" cy="424" r="10" style="fill: orange;"></circle>
        <circle id="Horse8" cx="639" cy="467" r="10" style="fill: orange;"></circle>
    
    </svg>';
            
        $mainplayer = $_POST['pseudoInput'];
        
        
        $reqSqlInsertGame = "INSERT INTO games (IDhex, gamePic, player1) VALUES ('" . $idlob . "', '" . $gamepic . "', '" . $mainplayer . "')";
        
        if (($bddArBoard->query($reqSqlInsertGame)) == TRUE) {
            echo "<br>Ajout effectué !";
        } else {
            echo "Ajout non effectué";
        }

        $reqSqlInsertFirstM = "INSERT INTO chat (IDhexGame, pseudo, message) VALUES ('" . $idlob . "', 'Admin', 'Bienvenue !')";
        
        if (($bddArBoard->query($reqSqlInsertFirstM)) == TRUE) {
            echo "<br>Ajout effectué !";
        } else {
            echo "Ajout non effectué";
        }

        echo "Lobby créé !";
        ?>
        <script>
            window.location.href="../../lobby.php?idgame=<?php echo $idlob ?>";
        </script>
    </body>
</html>
