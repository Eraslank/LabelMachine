using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostImage
{
    public class Label
    {
        public string labelName { get; set; }
    }
    public class Root
    {
        public string fileName { get; set; }
        public string base64Image { get; set; }
        public List<Label> labels { get; set; }
    }
}
