﻿namespace Assent.Reporters.DiffPrograms;

public interface IDiffProgram
{
    bool Launch(string receivedFile, string approvedFile);
}