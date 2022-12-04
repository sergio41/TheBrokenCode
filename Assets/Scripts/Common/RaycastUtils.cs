using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RaycastUtils
    {
        public static bool IsVisible(Transform agent, Vector2 m_Direction, Transform otherAgent, int angleDetected, float range, string targetTag, List<string> tagsToIgnore) {
            Vector2 targetDir = otherAgent.position - agent.position;
            float angle = Vector2.Angle(targetDir, m_Direction);
            if (Mathf.Abs(angle) < angleDetected) {
                var listOfObjectsVisibles = Physics2D.RaycastAll(agent.position, targetDir, range);
                foreach (RaycastHit2D hit in listOfObjectsVisibles)
                {
                    if (!tagsToIgnore.Contains(hit.transform.gameObject.tag) && !31.Equals(hit.transform.gameObject.layer))
                    {
                        return hit.transform.CompareTag(targetTag);
                    }
                }
            }
            return false;
        }
    }
}
