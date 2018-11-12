﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackController : MonoBehaviour
{
    public CardDeck player_deck;
    public CardDeck deck;
    PlayerControl pc_alice;
   
    private int blackjack_score;
    public int[] kind = {0, 0, 0, 0};
    // 0 = hearts, 1 = diamonds, 2 = clubs, 3 = spades.
    // private int hearts;
    // private int diamonds;
    // private int clubs;
    // private int spades;
    private bool over = false;
    public Text blackjack_text_score;
    public Text burst_meter_text;

    private void Start() {
        GameObject alice_object = GameObject.FindWithTag("Player");
        pc_alice = alice_object.GetComponent<PlayerControl>();
        blackjack_score = 0;
        blackjack_text_score.text = "score: " + blackjack_score;
    }
    void Update() {
        if(blackjack_score > 21) {
            over = true;
        }
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Y"))  {
            Hit();
        }
        if(Input.GetKeyDown(KeyCode.X) ||Input.GetButtonDown("B")) {
            Vent();
        }
        BurstDisplay();
    }

    void Vent() {
        if(over) {
            pc_alice.takeDamage();
            over = false;
        }
        while(player_deck.HasCards) {
            player_deck.BurstFiller(kind, blackjack_score);
            deck.Push(player_deck.Pop());
        }
        blackjack_score = 0;
        blackjack_text_score.text = "score: " + blackjack_score;
    }
    void Hit() {
        if(over) {
            pc_alice.takeDamage();
            over = false;
            Vent();
        } else {
            player_deck.Push(deck.Pop());
            blackjack_score = player_deck.HandValue();
            blackjack_text_score.text = "score: " + blackjack_score;    
        }
    }
    void BurstDisplay() {
        burst_meter_text.text = "hearts: " + kind[0] + "\ndiamonds: " + kind[1] + "\nclubs: " + kind[2] + "\nspades: " + kind[3]; 
    }
}
