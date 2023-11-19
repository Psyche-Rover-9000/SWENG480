using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class elementTest : MonoBehaviour
{

    //tests the getElement funtion.
    [UnityTest]
    public IEnumerator ElementCollectTest()
    {
        GameObject testObj = new GameObject("cat");
        testObj.AddComponent<IronScript>();
        IronScript testElement = testObj.GetComponent<IronScript>();
        Assert.IsNotNull(testElement);
        Assert.IsTrue(testElement.isActiveAndEnabled);
        int value = testElement.getElement();
        Assert.IsFalse(testElement.isActiveAndEnabled);
        Assert.IsTrue(value == 3);
        yield return null;
    }


}