﻿namespace MilitaryElite.Models
{
    public interface IMission
    {
        string CodeName { get; }
        string State { get; }
        void CompleteMission();
    }
}
