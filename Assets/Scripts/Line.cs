using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class Line : MonoBehaviour
    {
        // Reference
        public LineRenderer LineRenderer;
        public EdgeCollider2D edgeCol;

        
        private List<Vector2> points;
        // Start is called before the first frame update
        void Start()
        {
            LineRiderGameManager.Instance.lines.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateLine(Vector2 mousePos)
        {
            if (points == null)
            {
                points = new List<Vector2>();
                SetPoint(mousePos);
            }

            if (Vector2.Distance(points.Last(), mousePos)> .1f)
            {
                print("draw line");

                if (LineRiderGameManager.Instance.DecreaseInk(1)) { 
                    SetPoint(mousePos);    // Set a point to enlonger the line 
                }

            }
        }

        private void SetPoint(Vector2 point)
        {
            points.Add(point);
            LineRenderer.positionCount = points.Count;
            LineRenderer.SetPosition(points.Count - 1, point);

            if (points.Count > 1)
            {
                edgeCol.points = points.ToArray();
            }
        }

        public void Clear()
        {
            Destroy(gameObject);
        }
    }
}
