Feature: Ajout_Client
	En tant qu’administrateur du logiciel,
	je veux créer des comptes clients 
	afin de permettre aux clients d’accéder au logiciel

@mytag
Scenario: Ajouter_un_nouveau_client_avec_succès
    Given     je suis sur la page d’accueil du logiciel1
    And       je click sur se connecter1
    And       je saisis le nom d’utilisateur suivant1 :  'lionel'
    And       je saisis le mot de passe suivant1 : 'lionelo05'
    And       je clique sur le bouton connexion1
	And       je suis dans l'espace administrateur1
	And       je click sur Gerer comptes clients
	And       la banque a un total de 3 client

    When      je clique sur ajouter un nouveau client.
    And       je saisis le login 'Ricardo', le mot de passe, le nom, le prénom, 'alex@yahoo.fr' et d’autres informations du client X.
    And       je clique sur submit.

    Then      la banque a  maintenant un total de 4 client
    And       le client 'X' apparait dans la liste des clients



