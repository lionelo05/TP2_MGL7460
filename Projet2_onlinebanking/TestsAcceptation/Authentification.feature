Feature: Authentification
	En tant qu’administrateur ou client, 
	je veux m’authentifier afin 
	de pouvoir ajouter un client ou crediter/debiter mon compte.

@mytag
Scenario: Authentification_Réussie_Administrateur.
	Given je suis sur la page d’accueil du logiciel
    And   je click sur se connecter

    When  je saisis le nom d’utilisateur suivant :  'lionel'
    And   je saisis le mot de passe suivant : 'lionelo05'
    And   je clique sur le bouton connexion

    Then  Authentification 'réussie et je suis dirigé vers mon espace d’administrateur'

Scenario: Authentification_Echec_Administrateur_Casse_Non_Respect.
	Given je suis sur la page d’accueil du logiciel
    And   je click sur se connecter

    When  je saisis le nom d’utilisateur suivant :  'LIONEL'
    And   je saisis le mot de passe suivant : 'LIONELO05'
    And   je clique sur le bouton connexion

    Then  Authentification 'administrateur échouée et je reste sur la page de login'

Scenario: Authentification_Réussie_Client.
	Given je suis sur la page d’accueil du logiciel
    And   je click sur se connecter
    When  je saisis le nom d’utilisateur suivant :  'LeProf'
    And   je saisis le mot de passe suivant : 'lionelo05'
    And   je clique sur le bouton connexion
    Then  Authentification 'réussie et je suis dirigé vers mon espace client'


Scenario: Authentification_Echec_Client_Inconnu.
	Given je suis sur la page d’accueil du logiciel
    And   je click sur se connecter

    When  je saisis le nom d’utilisateur suivant :  'inconnu'
    And   je saisis le mot de passe suivant : 'inconnu'
    And   je clique sur le bouton connexion

    Then  Authentification 'client échouée et je reste sur la page de login'