using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour 
{
	public static void Inject(ref string s)
	{
		if (!s.Contains("["))
			return;

		//replace the tag
        s = s.Replace("[mom_eng]", "Mom");
        s = s.Replace("[mom_jpn]", "ママ");
        s = s.Replace("[dad_eng]", "Dad");
        s = s.Replace("[dad_jpn]", "パパ");
        s = s.Replace("[aunt_eng]", "Aunt");
        s = s.Replace("[aunt_jpn]", "おばさん");
        s = s.Replace("[teach_id]", "Bu Mina");
        s = s.Replace("[teach_eng]", "Ms. Mina");
        s = s.Replace("[teach_jpn]", "ミルナ先生");
        s = s.Replace("[kidm_id]", "Ibu Rara");
        s = s.Replace("[kidm_eng]", "Rara's Mom");
        s = s.Replace("[kidm_jpn]", "ララのママ");
        s = s.Replace("[officer_id]", "Tim SAR");
        s = s.Replace("[officer_eng]", "Rescue Team");
        s = s.Replace("[officer_jpn]", "救助隊");
    }

	public static string[] SplitByTags(string targetText)
	{
		return targetText.Split(new char[2]{'<','>'});
	}
}
