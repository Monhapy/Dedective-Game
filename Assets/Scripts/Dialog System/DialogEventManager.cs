using System;
using System.Collections.Generic;
public enum DialogEvent
{
    OnInitializedDialog,           // Ekranı karartma
    OnSetScene,                    // NPC kamera ve Oyuncunun pozisyonunu ayarlama
    OnStartQuestionStage,          // Ekrana soruları getirme   
    OnStartAnswerStage,            // Ekrana cevapları getirme 
    OnExitDialog                   // Diyalogdan çıkma
   
}

public static class DialogEventManager 
{
    private static readonly Dictionary<DialogEvent, Action> EventTable = new Dictionary<DialogEvent, Action>();


    public static void AddHandler(DialogEvent dialogEvent, Action dialogAction)
    {
        if (!EventTable.ContainsKey(dialogEvent))
            EventTable[dialogEvent] = dialogAction;
        else
            EventTable[dialogEvent] += dialogAction;
        //TryGetValue TryAddValue
    }

    public static void RemoveHandler(DialogEvent dialogEvent, Action dialogAction)
    {
        if (EventTable.ContainsKey(dialogEvent))
        {
            EventTable[dialogEvent] -= dialogAction;
            if (EventTable[dialogEvent] == null)
            {
                EventTable.Remove(dialogEvent);
            }
        }
    }

    public static void Broadcast(DialogEvent dialogEvent)
    {
        if (EventTable.ContainsKey(dialogEvent) && EventTable[dialogEvent] != null)
        {
            EventTable[dialogEvent]();
        }

    }
    public static bool HasEventHandler(DialogEvent dialogEvent)
    {
        return EventTable.ContainsKey(dialogEvent) && EventTable[dialogEvent] != null;
    }
    
}
