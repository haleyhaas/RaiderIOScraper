
public class Character
{
    public string name { get; set; }
    public string race { get; set; }
    public string Class { get; set; }
    public string active_spec_name { get; set; }
    public string active_spec_role { get; set; }
    public string gender { get; set; }
    public string faction { get; set; }
    public int achievement_points { get; set; }
    public int honorable_kills { get; set; }
    public string thumbnail_url { get; set; }
    public string region { get; set; }
    public string realm { get; set; }
    public DateTime last_crawled_at { get; set; }
    public string profile_url { get; set; }
    public string profile_banner { get; set; }
    public Mythic_Plus_Ranks mythic_plus_ranks { get; set; }
}

public class Mythic_Plus_Ranks
{
    public Overall overall { get; set; }
    public Class1 _class { get; set; }
    public Tank tank { get; set; }
    public Class_Tank class_tank { get; set; }
    public Healer healer { get; set; }
    public Class_Healer class_healer { get; set; }
    public Dps dps { get; set; }
    public Class_Dps class_dps { get; set; }
    public Spec_102 spec_102 { get; set; }
    public Spec_103 spec_103 { get; set; }
    public Spec_104 spec_104 { get; set; }
    public Spec_105 spec_105 { get; set; }
}

public class Overall
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class1
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Tank
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class_Tank
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Healer
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class_Healer
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Dps
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Class_Dps
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_102
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_103
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_104
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}

public class Spec_105
{
    public int world { get; set; }
    public int region { get; set; }
    public int realm { get; set; }
}
