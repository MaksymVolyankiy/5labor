using System;

public abstract class Disk
{
    public string Name { get; set; }
    public double Size { get; set; }

    public Disk(string name, double size)
    {
        Name = name;
        Size = size;
    }

    public abstract void DisplayInfo();
}

public class Directory : Disk
{
    public List<Disk> Contents { get; set; }

    public Directory(string name, double size) : base(name, size)
    {
        Contents = new List<Disk>();
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Directory: {Name}, Size: {Size}MB");
    }

    public void AddItem(Disk item)
    {
        Contents.Add(item);
    }

    public int CountAudioFiles()
    {
        int count = 0;
        foreach (Disk item in Contents)
        {
            if (item is Mp3 || item is Archive)
            {
                count++;
            }
            else if (item is Directory)
            {
                count += ((Directory)item).CountAudioFiles();
            }
        }
        return count;
    }
}

public class File : Disk
{
    public File(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"File: {Name}, Size: {Size}MB");
    }
}

public class Archive : Directory
{
    public Archive(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Archive: {Name}, Size: {Size}MB");
    }
}

public class Mp3 : File
{
    public Mp3(string name, double size) : base(name, size)
    {
    }

    public void Play()
    {
        Console.WriteLine($"Playing MP3 file: {Name}");
    }
}

public class Docx : File
{
    public Docx(string name, double size) : base(name, size)
    {
    }
    public override void DisplayInfo()
    {
        Console.WriteLine($"Docx File: {Name}, Size: {Size}MB");
    }
}


class Program
{
    static void Main(string[] args)
    {
        Directory root = new Directory("Root", 0);
        Directory music = new Directory("Music", 0);
        File textFile = new File("TextFile.txt", 1.5);
        Archive backup = new Archive("Backup", 10);
        Mp3 song1 = new Mp3("First Song.mp3", 5);
        Mp3 song2 = new Mp3("Second Song.mp3", 6);
        Docx document = new Docx("Document.docx", 2.5);

        root.AddItem(textFile);
        root.AddItem(music);
        music.AddItem(song1);
        music.AddItem(song2);
        root.AddItem(backup);
        root.AddItem(document);

        root.DisplayInfo();
        Console.WriteLine($"Number of audio files: {root.CountAudioFiles()}");
        song1.Play();
    }
}
