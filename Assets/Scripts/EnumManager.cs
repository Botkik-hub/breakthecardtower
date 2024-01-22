using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyType
{
    Enemy1,
    Enemy2,
    Enemy3,
    Enemy4,
    Enemy5,
    NumEnemyTypes
}
public enum EGameState
{
    PlayerTurn,
    OpponentTurn,
    Pause,
    Winning,
    Losing
}
public enum EPhase
{
    Draw,
    Main,
    Activation,
    Timer,
    End
}

public enum ESide
{
    Common,
    Player,
    Opponent
}

public enum ERarity
{
    Paper,
    Aluminum,
    Gold
};

public enum ECardType
{
    Permanent,
    Whimsy,
    TerraHex
}

public enum EWhimsyType
{
    Magic,
    Enchant,
    Time
};

public enum EPermanentType
{
    Artifact,
    NexusBobble
}

public enum EHexType
{
    None,
    Generic,
    Field,
    Lake,
    Mountain,
    Forest
}

public enum DeckType
{
    Generic,
    Abyss_Walker,
    Spelunker,
    Time_Wizard,
    Enemy1,
    Enemy2,
    Custom
}

public enum EEffectTiming
{
    Activate, 
    Aura,
    Demolish
}

public enum EEffectType
{
    Activate,
    ActivateAdjacent,
    Damage,
    Destroy,
    Draw
}