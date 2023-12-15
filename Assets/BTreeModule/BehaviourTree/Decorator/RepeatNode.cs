using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
// Classe RepeatNode
// Cette classe est une sp�cialisation de DecoratorNode et repr�sente un n�ud d�corateur qui r�p�te continuellement l'ex�cution de son n�ud enfant.
// Le n�ud RepeatNode continue de renvoyer l'�tat Running, ce qui entra�ne une r�p�tition constante du comportement de l'enfant.
public class RepeatNode : DecoratorNode
{

    // Fonction OnStart
    // Impl�ment�e si une logique sp�ciale est n�cessaire au d�marrage du n�ud RepeatNode.
    protected override void OnStart()
    {
    }

    // Fonction OnStop
    // Impl�ment�e si une logique sp�ciale est n�cessaire � l'arr�t du n�ud RepeatNode.
    protected override void OnStop()
    {
    }

    // Fonction OnUpdate
    // � chaque mise � jour, ex�cute la mise � jour du n�ud enfant et retourne toujours l'�tat Running.
    // Cela cr�e un cycle de r�p�tition continuelle pour le n�ud enfant.
    protected override State OnUpdate()
    {

        m_child.Update();
        return State.Running;
    }
}
#endif