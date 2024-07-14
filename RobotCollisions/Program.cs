using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCollisions
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] positions = { 3, 5, 2, 6 };
            int[] healths = { 10, 10, 15, 12 };
            List<int> answer = new List<int>(SurvivedRobotsHealths(positions,healths, "RLRL"));
            foreach (var item in answer)
            {
                Console.Write(item + " ");
            }
            Console.ReadKey();
        }
        public static IList<int> SurvivedRobotsHealths(int[] positions, int[] healths, string directions)
        {
            int n = positions.Length;
            List<(int position, int health, char direction)> robots = new List<(int, int, char)>();

            for (int i = 0; i < n; i++)
            {
                robots.Add((positions[i], healths[i], directions[i]));
            }

            // Sort robots by their positions
            robots.Sort((a, b) => a.position.CompareTo(b.position));

            Stack<(int position, int health, char direction)> stack = new Stack<(int, int, char)>();
            stack.Push(robots[0]);
            var topRobot = stack.Peek(); 
            for (int i = 0; i< robots.Count;i++)
            {
                while (stack.Count > 0 && stack.Peek().direction == 'R' && robots[i].direction == 'L' && stack.Peek().position != topRobot.position)
                {
                    if (robots[i].health > stack.Peek().health)
                    {
                        stack.Pop();
                        robots[i] = (robots[i].position, robots[i].health - 1, robots[i].direction);
                        if (!(stack.Peek().direction == 'R' && robots[i].direction == 'L')|| stack.Count == 0)
                        {
                            stack.Push(robots[i]);
                        }                      
                    }
                    else if(robots[i].health < stack.Peek().health)
                    {
                        topRobot = (stack.Peek().position, stack.Peek().health - 1, stack.Peek().direction);
                        stack.Pop();
                        stack.Push(topRobot);
                    }
                    else
                    {
                        stack.Pop();
                        break;
                    }
                }
                if ((i + 1 == robots.Count)&& stack.Count == 0)
                {
                    return new List<int>();
                }
                if (stack.Count == 0 )
                {
                    stack.Push(robots[i + 1]);
                }
                if (!(stack.Peek().direction == 'R' && robots[i].direction == 'L')&&stack.Peek().position != robots[i].position)
                {
                    stack.Push(robots[i]);
                }
            }

            List<int> survivedHealths = new List<int>();
            foreach (var robot in stack)
            {
                survivedHealths.Add(robot.health);
            }

            return survivedHealths;
        }

    }
}
