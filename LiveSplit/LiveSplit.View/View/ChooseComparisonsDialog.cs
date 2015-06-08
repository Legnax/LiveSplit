﻿using LiveSplit.Model.Comparisons;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.View
{
    public partial class ChooseComparisonsDialog : Form
    {
        public IDictionary<string, bool> ComparisonGeneratorStates { get; set; }
        protected bool DialogInitialized;

        public ChooseComparisonsDialog()
        {
            InitializeComponent();
            DialogInitialized = false;
            comparisonsListBox.Items.AddRange(new []
            {
                BestSegmentsComparisonGenerator.ComparisonName,
                BestSplitTimesComparisonGenerator.ComparisonName,
                AverageSegmentsComparisonGenerator.ComparisonName,
                WorstSegmentsComparisonGenerator.ComparisonName,
                PercentileComparisonGenerator.ComparisonName,
                NoneComparisonGenerator.ComparisonName
            });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comparisonsListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (DialogInitialized)
            {
                var generatorName = (string)comparisonsListBox.Items[e.Index];
                if (ComparisonGeneratorStates.ContainsKey(generatorName))
                    ComparisonGeneratorStates[generatorName] = e.NewValue == CheckState.Checked;
                else
                {
                    ComparisonGeneratorStates.Clear();
                    foreach (var item in comparisonsListBox.Items)
                    {
                        if ((string)item == generatorName)
                            ComparisonGeneratorStates[generatorName] = e.NewValue == CheckState.Checked;
                        else
                            ComparisonGeneratorStates[(string)item] = comparisonsListBox.GetItemChecked(comparisonsListBox.Items.IndexOf(item));
                    }
                }
            }
        }

        private void ChooseComparisonsDialog_Load(object sender, EventArgs e)
        {
            foreach (var generator in ComparisonGeneratorStates)
            {
                comparisonsListBox.SetItemChecked(comparisonsListBox.Items.IndexOf(generator.Key), generator.Value);
            }
            DialogInitialized = true;
        }
    }
}
