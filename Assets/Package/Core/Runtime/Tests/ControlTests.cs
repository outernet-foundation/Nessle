// using UnityEngine;

// using NUnit.Framework;
// using ObserveThing;
// using static Nessle.UIBuilder;
// using System.Linq;
// using System.Collections.Generic;

// namespace Nessle.Tests
// {
//     public class ControlTests
//     {
//         [Test]
//         public void TestSetParent()
//         {
//             IControl control1 = default;
//             IControl control2 = default;

//             try
//             {
//                 control1 = new Control("control1");
//                 control2 = new Control("control2");

//                 control2.parent.From(control1);

//                 Assert.AreEqual(1, control1.children.count);
//                 Assert.AreEqual(control1, control2.parent.value);
//                 Assert.IsTrue(control1.children.Contains(control2));
//             }
//             finally
//             {
//                 control1?.Dispose();
//                 control2?.Dispose();
//             }
//         }

//         [Test]
//         public void TestAddChildren()
//         {
//             IControl control1 = default;
//             IControl control2 = default;
//             IControl control3 = default;
//             IControl control4 = default;

//             try
//             {
//                 control1 = new Control("control1");
//                 control2 = new Control("control2");
//                 control3 = new Control("control3");
//                 control4 = new Control("control4");

//                 control1.children.Add(control2);
//                 control1.children.Add(control3);
//                 control1.children.Add(control4);

//                 Assert.AreEqual(3, control1.children.count);
//                 Assert.AreEqual(control1, control2.parent.value);
//                 Assert.AreEqual(control1, control3.parent.value);
//                 Assert.AreEqual(control1, control4.parent.value);
//                 Assert.AreEqual(control2, control1.children[0]);
//                 Assert.AreEqual(control3, control1.children[1]);
//                 Assert.AreEqual(control4, control1.children[2]);
//             }
//             finally
//             {
//                 control1?.Dispose();
//                 control2?.Dispose();
//                 control3?.Dispose();
//                 control4?.Dispose();
//             }
//         }

//         [Test]
//         public void TestInsertChild()
//         {
//             IControl control1 = default;
//             IControl control2 = default;
//             IControl control3 = default;

//             try
//             {
//                 control1 = new Control("control1");
//                 control2 = new Control("control2");
//                 control3 = new Control("control3");

//                 control1.children.Add(control2);
//                 control1.children.Insert(0, control3);

//                 Assert.AreEqual(2, control1.children.count);
//                 Assert.AreEqual(control1, control2.parent.value);
//                 Assert.AreEqual(control1, control3.parent.value);
//                 Assert.AreEqual(control3, control1.children[0]);
//                 Assert.AreEqual(control2, control1.children[1]);
//             }
//             finally
//             {
//                 control1?.Dispose();
//                 control2?.Dispose();
//                 control3?.Dispose();
//             }
//         }

//         [Test]
//         public void TestAddChildrenFromObservable_AddAfterFrom()
//         {
//             IControl control1 = default;
//             ListObservable<IControl> childrenFrom = new ListObservable<IControl>();

//             try
//             {
//                 control1 = new Control("control1");
//                 control1.children.From((IListObservable<IControl>)childrenFrom);

//                 childrenFrom.Add(new Control("control2"));
//                 childrenFrom.Add(new Control("control3"));
//                 childrenFrom.Add(new Control("control4"));
//                 childrenFrom.Add(new Control("control5"));
//                 childrenFrom.Add(new Control("control6"));

//                 Assert.AreEqual(childrenFrom, control1.children);
//             }
//             finally
//             {
//                 control1?.Dispose();
//             }
//         }

//         [Test]
//         public void TestAddChildrenFromObservable_AddBeforeFrom()
//         {
//             IControl control1 = default;
//             ListObservable<IControl> childrenFrom = new ListObservable<IControl>();

//             try
//             {
//                 control1 = new Control("control1");

//                 childrenFrom.Add(new Control("control2"));
//                 childrenFrom.Add(new Control("control3"));
//                 childrenFrom.Add(new Control("control4"));
//                 childrenFrom.Add(new Control("control5"));
//                 childrenFrom.Add(new Control("control6"));

//                 control1.children.From((IListObservable<IControl>)childrenFrom);

//                 Assert.AreEqual(childrenFrom, control1.children);
//             }
//             finally
//             {
//                 control1?.Dispose();
//             }
//         }
//     }
// }