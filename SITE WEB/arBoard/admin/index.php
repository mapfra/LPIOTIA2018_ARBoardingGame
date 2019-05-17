<html>
	<head>
        <meta charset="utf-8" />
        <!-- <link rel="stylesheet" type="text/css" href="../base/css/game.css"> -->
        <link rel="stylesheet" type="text/css" href="../base/css/generic.css">
        <title>Ar Boarding Game</title>
    	</head>
	<body>
	<div id="main_wrapper" class="flex-container-column">
            <?php include('../base/include/header.php'); ?>
            <div id="content" class="flex-container-row">
                <div id="pageContent">              
					<?php
						include '../base/include/connexionBDD.php';
						$selectUsers = $bddArBoard->query('SELECT * FROM users');
								?><table>
						<tr>
							<th>email</th>
							<th>pseudo</th>
						</tr>
						<?php
						while ($donnees = $selectUsers->fetch()) {
						?>
									<tr>
							<td>
						<?php	echo $donnees['email']; ?>
							</td>
							<td>
						<?php	echo $donnees['pseudo']; ?>
							</td>
							</tr>
						<?php
								}
						echo "</table>";
					?>
                </div>
            </div>
        </div>
	</body>
</html>
