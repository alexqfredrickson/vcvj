# vcvj
![Screenshot](demo/demo%20-%20shuffled.gif)

## A brief introduction
**vcvj** is a .NET 8.0 (C#) framework for tokenizing and transforming GIF files.

### *Tokenization*

*vcvj* parses GIF files into semantic components defined by the wonderful and frightening [gif89a spec](https://www.w3.org/Graphics/GIF/spec-gif89a.txt) - yielding a `VcvjImage` object which contains:

- Data blocks
- Graphic blocks
- Table-based images
- Application extensions
- *And more!*

### *Transformation*

Once the GIF is parsed into a `VcvjImage` object, the `VcvjImage` API is used to alter the image's grammatical components and save it back into a GIF, with features such as:

- Randomizing image frames

```
vcvj.DataStream.RandomizeFrames();
```

![Screenshot](demo/demo%20-%20shuffled.gif)

- Altering color tables

```
vcvj.DataStream.LogicalScreen.GlobalColorTable.XOR(50);
```

![Screenshot](demo/demo%20-%20colors%20inverted%20(XOR%2050).gif)

```
vcvj.DataStream.LogicalScreen.GlobalColorTable.XOR(100);
```

![Screenshot](demo/demo%20-%20colors%20inverted%20(XOR%20100).gif)

```
vcvj.DataStream.LogicalScreen.GlobalColorTable.Randomize();
```

![Screenshot](demo/demo%20-%20colors%20partially%20randomized.gif)

```
vcvj.DataStream.LogicalScreen.GlobalColorTable.RandomizeHalf();
```

![Screenshot](demo/demo%20-%20colors%20randomized.gif)

- Glitching them out beyond comprehension and effectively destroying them

```
vcvj.DataStream.DeleteRandomCompiledBytes(75000);
```

![Screenshot](demo/demo%20-%2075000%20bytes%20liberated.gif)
![Screenshot](demo/demo%20-%20100000%20bytes%20liberated.gif)
![Screenshot](demo/demo%20-%20125000%20bytes%20liberated.gif)

(Note: If you're using Chrome or Firefox, you probably can't see the images above, because modern web browsers perform some kind of sanity check upon encountering malformed GIFs.  Luckily, IE and MS Edge don't:)

![Screenshot](demo/demo%20-%20ms%20ie%20rendering.png)

- *And more!*

## Configuration

To configure and run *vcvj*:

1. Create copies of the `app.config.example` files in the `vcvj` and `vcvj.Tests` projects, name them both `app.config`, and set the constants (such as `WorkingDirectory`) as-needed.
2. Compile and build the solution.

To generate modified GIFs on-the-fly, run the unit tests located in the `vcvj.Tests` project.
