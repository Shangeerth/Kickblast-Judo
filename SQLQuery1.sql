CREATE TABLE [dbo].[AthleteCompetitionTrainingPlan] (
    AthleteCompetitionID INT NOT NULL,
    PlanID INT NOT NULL,
    CONSTRAINT FK_ACTP_AC FOREIGN KEY (AthleteCompetitionID) REFERENCES AthleteCompetition(AthleteCompetitionID),
    CONSTRAINT FK_ACTP_TP FOREIGN KEY (PlanID) REFERENCES TrainingPlan(PlanID)
);
