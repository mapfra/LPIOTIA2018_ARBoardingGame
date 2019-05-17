<?php
                    $idlob = $_GET['idgame'];
                    if(isset($_GET['pseudoPlayer'])){
                        $pseudoPlayer = $_GET['pseudoPlayer'];
                        $selectGame = $bddArBoard->query("SELECT * FROM games WHERE IDhex LIKE '" . $idlob . "'");
                        foreach($selectGame as $value) {
                            $board = $value[1];
                            $player1 = $value[2];
                            $player2 = $value[3];
                            $player3 = $value[4];
                            $player4 = $value[5];
                        }
                        if(($pseudoPlayer == $player1) || ($pseudoPlayer == $player2) || ($pseudoPlayer == $player3) || ($pseudoPlayer == $player4)) {
                            // echo "Joueur déjà présent dans le lobby";
                        } else {
                            if($player2 == NULL){
                                $reqSqlUpdateP2 = "UPDATE `games` SET `player2` = '" . $pseudoPlayer . "'";
                                if (($bddArBoard->query($reqSqlUpdateP2)) == TRUE) {
                                    echo "Lobby join";
                                    $player2 = $pseudoPlayer;
                                } else {
                                    echo "Error join lobby p1";
                                }
                            } else if($player3 == NULL){
                                $reqSqlUpdateP3 = "UPDATE `games` SET `player3` = '" . $pseudoPlayer . "'";
                                if (($bddArBoard->query($reqSqlUpdateP3)) == TRUE) {
                                    echo "Lobby join";
                                    $player3 = $pseudoPlayer;
                                } else {
                                    echo "Error join lobby p2";
                                }
                            } else if($player4 == NULL){
                                $reqSqlUpdateP4 = "UPDATE `games` SET `player4` = '" . $pseudoPlayer . "'";
                                if (($bddArBoard->query($reqSqlUpdateP4)) == TRUE) {
                                    echo "Lobby join";
                                    $player4 = $pseudoPlayer;
                                } else {
                                    echo "Error join lobby p3";
                                }
                            } else {
                                echo "The limit number of players is reached.";
                            }
                        }
                    } else {
                ?>
                <script>
                    window.location.href="./app/lobby/joinLobby.php?idgame=<?php echo $idlob ?>";
                </script>
                <?php
                    }
                ?>