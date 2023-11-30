using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPrioritizationManager : MonoBehaviour
{
    public GroupManager[] GroupManagersList;
    public int selectedGroup = 0; // default to be 0. When it's 0, no prioritization is applied
    // Start is called before the first frame update
    void Start()
    {
        if (GroupManagersList.Length == 0) {
            GroupManagersList = GetComponentsInChildren<GroupManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectedGroupChange(int newSelectedGroup) {
        if (newSelectedGroup == 0)
        {
            foreach (GroupManager gm in GroupManagersList)
            {
                gm.ResetVolumeForGroup();
            }
        }
        else if (selectedGroup == 0)
        {
            for (int i = 0; i < GroupManagersList.Length; i++)
            {
                if (i != newSelectedGroup - 1)
                {
                    GroupManager gm = GroupManagersList[i];
                    gm.LowerVolumeForGroup();
                }
            }
        }
        else {
            GroupManagersList[selectedGroup - 1].LowerVolumeForGroup();
            GroupManagersList[newSelectedGroup - 1].ResetVolumeForGroup();
        }

        selectedGroup = newSelectedGroup;
    }
}
