Console.WriteLine(new List<string[]>(){File.ReadAllLines("input.txt")}
    .Select(j => j.Select(line => line.Select<char,bool[]>(c => c == '|' ? [true,false,true,false] : c == '-' ? [false,true,false,true] : c == 'L' ? [true,true,false,false] : c == 'J' ? [true,false,false,true] : c == '7' ? [false,false,true,true] : c == 'F' ? [false,true,true,false] : c == '.' ? [false,false,false,false] : [true,true,true,true])))
    .Select(j => new Dictionary<Tuple<int,int>, bool[]>(j.SelectMany((line,row) => line.Select((c,column) => new KeyValuePair<Tuple<int,int>,bool[]>(new(column,row), c)))))
    .Select(j => new{dict = j, s = j.First(c => c.Value.All(t => t)).Key})
    .Select(j => new{j.s, dict = new Dictionary<Tuple<int,int>, bool[]>(j.dict.Keys.Select(c =>
        new KeyValuePair<Tuple<int, int>, bool[]>(c, [j.dict[c][0] && j.dict.ContainsKey(new(c.Item1,c.Item2-1)) && j.dict[new(c.Item1,c.Item2-1)][2], j.dict[c][1] && j.dict.ContainsKey(new(c.Item1+1,c.Item2)) && j.dict[new(c.Item1+1, c.Item2)][3], j.dict[c][2] && j.dict.ContainsKey(new(c.Item1,c.Item2+1)) && j.dict[new(c.Item1, c.Item2+1)][0], j.dict[c][3] && j.dict.ContainsKey(new(c.Item1-1,c.Item2)) && j.dict[new(c.Item1-1,c.Item2)][1]])))})
    .Select(j => Enumerable.Range(0, j.dict.Keys.Count).Aggregate(new List<Tuple<int,int>>(){j.s}, (previous, current) => 
    {
        var aux = previous.Last();
        var available = new List<Tuple<int,int>>();
        if(j.dict[aux][0])
            available.Add(new(aux.Item1, aux.Item2-1));
        if(j.dict[aux][1])
            available.Add(new(aux.Item1+1, aux.Item2));
        if(j.dict[aux][2])
            available.Add(new(aux.Item1, aux.Item2+1));
        if(j.dict[aux][3])
            available.Add(new(aux.Item1-1, aux.Item2));
        available = available.Where(c => !previous.Contains(c)).ToList();
        if(available.Any())
            return [..previous,available.First()];
        return previous;
    }))
    .Select(j => new{one = j.Skip(1).Select((c , index) => index+1), other = j.Skip(1).Reverse().Select((c,index) => index+1).Reverse()})
    .Select(j => j.one.Zip(j.other))
    .First().Max(t => Math.Min(t.First, t.Second)));