using System;
using System.Threading;
using MiddleLevelDevCourse.Extensions;

namespace MiddleLevelDevCourse.Playground
{
    [Flags]
    public enum Playgrounds
    {
        Common = 0x0001,
        Volatile = 0x0002,
        Reflection = 0x0004
    }

    public class Playground : ILesson
    {
        private readonly Playgrounds _playgrounds;

        public Playground(Playgrounds playgrounds)
        {
            _playgrounds = playgrounds;
        }

        public void Run()
        {
            foreach(var playground in _playgrounds.GetFlags())
            {
                switch (playground)
                {
                    case Playgrounds.Common:
                        CommonPlayground();
                        break;
                    case Playgrounds.Volatile:
                        VolatilePlayground();
                        break;
                    case Playgrounds.Reflection:
                        ReflectionPlayground();
                        break;
                    default:
                        continue;
                }
            }
        }

        private static void CommonPlayground()
        {
            dynamic dyn = new Random().Next(1, 10) >= 5 ? new CustomAttribute(0) : new SomeClass();
        }

        #region Volatile

        private int _index;

        // Field might be modified by multiple threads that are executing at the same time.
        // Guarantees that all threads will observe volatile writes performed by any other thread in the order in which they were performed.
        // For double or float (for example) sould be used Interlocked or lock construction.
        // Disallows compiler to optimize (chache etc) volatile fields.
        private volatile int _indexVolatile;

        private void VolatilePlayground()
        {
            var thread1 = new Thread(() =>
            {
                _index = 1;
                _indexVolatile = 1;
            })
            {
                // Actually default values.
                IsBackground = false,
                Priority = ThreadPriority.Normal
            };

            var thread2 = new Thread(() =>
            {
                if (_index == 1)
                {
                    // We can use memory barrier to guarantee _index == 1.
                    Thread.MemoryBarrier();

                    // Can be zero.
                    Console.WriteLine(_index);
                }

                if (_indexVolatile == 1)
                {
                    Console.WriteLine(_indexVolatile);
                }
            });

            thread1.Start();
            thread2.Start();
        }

        #endregion

        private static void ReflectionPlayground()
        {
            var obj = new SomeClass();

            var attr = Attribute.GetCustomAttribute(obj.GetType(), typeof(CustomAttribute)) as CustomAttribute;

            Console.WriteLine(attr.Prop);
            Console.WriteLine(obj.NameOf);
        }
    }

    #region Types

    public class CustomAttribute : Attribute
    {
        public CustomAttribute(int prop)
        {
            Prop = prop;
        }

        public int Prop { get; }
    }

    [Custom(10)]
    public class SomeClass
    {
        public string NameOf { get; } = nameof(SomeClass);
    }

    #endregion
}
