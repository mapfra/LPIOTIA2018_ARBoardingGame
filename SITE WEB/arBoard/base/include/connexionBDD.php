        <?php    
            try {
                $bddArBoard = new PDO('mysql:host=localhost;dbname=ar_boarding_game;charset=utf8', 'root', 'rootpass');
            } catch (Exception $e) {
                die('Erreur : '.$e->getMessage());
            }
        ?>
