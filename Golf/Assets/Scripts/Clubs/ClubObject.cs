using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubObject : MonoBehaviour
{
    public Club m_club;

    private void Start() {
        InitializeClub(m_club);
    }


    public void InitializeClub(Club club) {
        m_club = club;

        Instantiate(m_club.clubArtObject, this.transform);

    }

}
